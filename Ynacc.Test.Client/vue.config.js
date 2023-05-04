'use strict'
const path = require('path')
const defaultSettings = require('./src/settings.js')

function resolve(dir) {
  return path.join(__dirname, dir)
}

const name = defaultSettings.title || 'DEMO' // page title

// If your port is set to 80,
// use administrator privileges to execute the command line.
// For example, Mac: sudo npm run
// You can change the port by the following method:
// port = 9527 npm run dev OR npm run dev --port = 9527
const port = process.env.port || process.env.npm_config_port || 6328 // dev port

var pages = {
  app: {
    entry: 'src/main.js',
    template: 'public/index.html',
    filename: 'index.html',
    excludeChunks: ['silentrenewoidc']

    // chunks: ['chunk-libs', 'chunk-commons','chunk-elementUI', 'runtime', 'app']
  },
  silentrenewoidc: {
    entry: 'src/silent-renew-oidc.js',
    template: 'public/silent-renew-oidc.html',
    filename: 'silent-renew-oidc.html',
    excludeChunks: ['app']
    // chunks: ['silentrenewoidc']
  }
}
if (process.env.NODE_ENV != 'development') {
  pages = {
    main: {
      entry: 'src/main.js',
      template: 'public/index.html',
      filename: 'index.html',
      excludeChunks: ['silentrenewoidc'],
      chunks: ['chunk-libs', 'chunk-commons', 'chunk-elementUI', 'runtime', 'main']
    },
    silentrenewoidc: {
      entry: 'src/silent-renew-oidc.js',
      template: 'public/silent-renew-oidc.html',
      filename: 'silent-renew-oidc.html',
      excludeChunks: ['main'],
      chunks: ['chunk-libs', 'runtime', 'silentrenewoidc']
    }
  }
}

// All configuration item explanations can be find in https://cli.vuejs.org/config/
module.exports = {
  /**
   * You will need to set publicPath if you plan to deploy your site under a sub path,
   * for example GitHub Pages. If you plan to deploy your site to https://foo.github.io/bar/,
   * then publicPath should be set to "/bar/".
   * In most cases please use '/' !!!
   * Detail: https://cli.vuejs.org/config/#publicpath
   */
  publicPath: '/',
  outputDir: 'dist',
  assetsDir: 'static',
  lintOnSave: process.env.NODE_ENV === 'development',
  productionSourceMap: false,
  devServer: {
    port: port,
    open: true,
    overlay: {
      warnings: false,
      errors: true
    },
    before: require('./mock/mock-server.js')
  },

  pages: pages,

  configureWebpack: {
    // provide the app's title in webpack's name field, so that
    // it can be accessed in index.html to inject the correct title.
    name: name,
    resolve: {
      alias: {
        '@': resolve('src')
      }
    }
  },
  chainWebpack(config) {
    // it can improve the speed of the first screen, it is recommended to turn on preload
    Object.keys(pages).forEach(name => {
      config.plugin(`preload-${name}`)
        .tap(() => [
          {
            rel: 'preload',
            // to ignore runtime.js
            // https://github.com/vuejs/vue-cli/blob/dev/packages/@vue/cli-service/lib/config/app.js#L171
            fileBlacklist: [/\.map$/, /hot-update\.js$/, /runtime\..*\.js$/, /silentrenewoidc\.js$/, /silentrenewoidc\..*\.js$/],
            include: 'initial'
          }
        ])
    })

    // config.plugin('preload').tap(() => [
    //   {
    //     rel: 'preload',
    //     // to ignore runtime.js
    //     // https://github.com/vuejs/vue-cli/blob/dev/packages/@vue/cli-service/lib/config/app.js#L171
    //     fileBlacklist: [/\.map$/, /hot-update\.js$/, /runtime\..*\.js$/],
    //     include: 'initial',
    //     exclude: "silent-renew-oidc.js"
    //   }
    // ])

    // when there are many pages, it will cause too many meaningless requests
    config.plugins.delete('prefetch')

    // set svg-sprite-loader
    config.module
      .rule('svg')
      .exclude.add(resolve('src/icons'))
      .end()
    config.module
      .rule('icons')
      .test(/\.svg$/)
      .include.add(resolve('src/icons'))
      .end()
      .use('svg-sprite-loader')
      .loader('svg-sprite-loader')
      .options({
        symbolId: 'icon-[name]'
      })
      .end()

    config
      .when(process.env.NODE_ENV !== 'development',
        config => {
          config
            .plugin('ScriptExtHtmlWebpackPlugin')
            .after('html')
            .use('script-ext-html-webpack-plugin', [{
            // `runtime` must same as runtimeChunk name. default is `runtime`
              inline: /runtime\..*\.js$/
            }])
            .end()
          config
            .optimization.splitChunks({
              chunks: 'all',
              cacheGroups: {
                libs: {
                  name: 'chunk-libs',
                  test: /[\\/]node_modules[\\/]/,
                  priority: 10,
                  chunks: 'initial' // only package third parties that are initially dependent
                },
                elementUI: {
                  name: 'chunk-elementUI', // split elementUI into a single package
                  priority: 20, // the weight needs to be larger than libs and app or it will be packaged into libs or app
                  test: /[\\/]node_modules[\\/]_?element-ui(.*)/ // in order to adapt to cnpm
                },
                oidc: {
                  name: 'chunk-oidc', // split elementUI into a single package
                  priority: 5, // the weight needs to be larger than libs and app or it will be packaged into libs or app
                  test: /[\\/]node_modules[\\/]_?oidc-client(.*)/ // in order to adapt to cnpm
                },
                vueOidc:
                {
                  name: 'chunk-voidc', // split elementUI into a single package
                  priority: 5, // the weight needs to be larger than libs and app or it will be packaged into libs or app
                  test: /[\\/]node_modules[\\/]_?vuex-oidc(.*)/ // in order to adapt to cnpm
                },
                commons: {
                  name: 'chunk-commons',
                  test: resolve('src/components'), // can customize your rules
                  minChunks: 3, //  minimum common number
                  priority: 5,
                  reuseExistingChunk: true
                }
              }
            })
          // https:// webpack.js.org/configuration/optimization/#optimizationruntimechunk
          config.optimization.runtimeChunk('single')
        }
      )
  }
}
