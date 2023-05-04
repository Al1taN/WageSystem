<template>
    <div>
      <div class="main"></div>
      <el-select v-model="value" placeholder="请选择数据查询类别">
        <el-option
          v-for="item in options"
          :key="item.value"
          :label="item.label"
          :value="item.value">
        </el-option>
      </el-select>
      <span v-if="this.roles[0] == 'Wage_Admin'">
        <el-select v-model="selectdept" placeholder="选择查询部门">
                <el-option
                v-for="item in depts"
                :key="item.value"
                :label="item.label"
                :value="item.value">
                </el-option>
        </el-select>
      </span>
      <span class="time">
        时间段： 从<input type="date" v-model="begin">
        到 <input type="date" v-model="end">
      </span>
      <span class="time">
        工资号： <el-input type="text" placeholder="默认不区分" maxlength=6 style="width: 10%;" show-word-limit v-model="number"></el-input>
      </span>
      <span class="gap"></span>
      <span class="time"><el-button @click="check" :loading = "loading">查询</el-button></span>
      <span class="time"><el-button :loading="downloadLoading" @click="handleDownload">导出EXCEL</el-button></span>
      <span class="time"><el-button @click="close">关闭</el-button></span>
      <el-table v-if="flag" :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
        <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" />
      </el-table>
    </div>
  </template>
  
  <script>
    import { mapGetters } from 'vuex'
    import {GetDept} from '@/api/dashboard'
    import { parseTime } from '@/utils'
    import {GetStaticsAttend,GetWorkStaticsAttend} from '@/api/statistics'
    export default {
      created:async function(){
        await GetDept().then(res => {this.$store.dispatch('user/getAchive', res)})
        var now = new Date();
        var nowmonth = now.getMonth() +1
        if(nowmonth < 10){nowmonth = '0' + nowmonth}
        var nowtime = now.getFullYear() + '-' + nowmonth + '-' + now.getDate()
        var nowtime1 = now.getFullYear() + '-' + nowmonth + '-01'
        this.begin = nowtime1
        this.end = nowtime
      },
      data() {
      return {
        options: [{
          value: '新版员工考勤统计',
          label: '新版员工考勤统计'
          }, {
            value: '新版劳务制员工考勤统计',
            label: '新版劳务制员工考勤统计'
          }, {
            value: '老版员工考勤统计',
            label: '老版员工考勤统计'
          }, {
            value: '老版劳务制员工考勤统计',
            label: '老版劳务制员工考勤统计'  
          }],
          depts: [ {
                  value: '00',
                  label: '所有部门:00'
                  },
                  {
                  value: '01',
                  label: '办公室:01'
                  }, {
                  value: '02',
                  label: '规划发展部:02'
                  }, {
                  value: '03',
                  label: '人力资源部:03'
                  }, {
                  value: '04',
                  label: '财务会计部:04'
                  }, {
                  value: '05',
                  label: '审计部:05'
                  }, {
                  value: '06',
                  label: '保卫部:06'
                  }, {
                  value: '07',
                  label: '运行管理部:07'
                  }, {
                  value: '08',
                  label: '服务与质量管理部:08'
                  }, {
                  value: '09',
                  label: '安全监察部:09'
                  }, {
                  value: '10',
                  label: '飞行技术管理部:10'
                  }, {
                  value: '11',
                  label: '综合管理部:11'
                  }, {
                  value: '12',
                  label: '运行控制部:12'
                  }, {
                  value: '13',
                  label: '客舱服务部:13'
                  }, {
                  value: '14',
                  label: '地面服务部:14'
                  }, {
                  value: '15',
                  label: '市场销售部:15'
                  }, {
                  value: '16',
                  label: '飞行部:16'
                  }, {
                  value: '17',
                  label: '货运部:17'
                  }, {
                  value: '18',
                  label: '昆明维修基地:18'
                  }, {
                  value: '19',
                  label: '党委工作部:19'
                  }, {
                  value: '20',
                  label: '离退休管理部:20'
                  }, {
                  value: '21',
                  label: '群团工作部:21'
                  }, {
                  value: '22',
                  label: '信息管理部:22'
                  }, {
                  value: '23',
                  label: '基本建设管理部:23'
                  }, {
                  value: '24',
                  label: '机场扩建协调指挥部:24'
                  }, {
                  value: '25',
                  label: '机关党委:25'
                  }, {
                  value: '26',
                  label: '纪委办公室:26'  
                  }, {
                  value: '27',
                  label: '团委:27'
                  }, {
                  value: '28',
                  label: '股份公司:28'
                  }, {
                  value: '29',
                  label: '北京分公司:29'
                  }, {
                  value: '30',
                  label: '东美公司:30'
                  }, {
                  value: '31',
                  label: '成都基地:31'
                  }, {
                  value: '32',
                  label: '丽江基地:32'
                  }, {
                  value: '33',
                  label: '局方飞行人员:33'
                  }, {
                  value: '34',
                  label: '驻国外:34'
                  }, {
                  value: '35',
                  label: '空警:35'
                  }, {
                  value: '36',
                  label: '内退:36'
                  }, {
                  value: '37',
                  label: '代垫工资:37'
                  }, {
                  value: '38',
                  label: '部门:38' 
                  }, {
                  value: '39',
                  label: '采购部:39'
                  }, {
                  value: '40',
                  label: '四川分公司:40'
                  }, {
                  value: '41',
                  label: '机务工程部:41'
                  }, {
                  value: '42',
                  label: '法律合规部:42'
                  }, {
                  value: '43',
                  label: '工会女工委:43'        
              }],
          selectdept: '',
          value: '',
          begin:'2022-10-01',
          end:'2022-11-17',
          tableData: [{'部门':1,'工资号':2,'姓名':3,'复核':4,'审核':5}],
          tableHeader: ['部门','工资号','姓名','复核','审核'],
          downloadLoading: false,
          autoWidth: true,
          bookType: 'xlsx',
          flag: false,
          number:'',
          loading:false
        } 
    },
    computed: {
            ...mapGetters([
            'Users','roles'
            ])
        },
    methods:{
      async check(){
        this.loading = true
        if(this.value && this.begin && this.end && this.end >= this.begin){
          this.flag = true
          this.tableData = []
          this.tableHeader = []
          if(this.number == ''){
            this.number = '000000'
          }
          var dept = this.Users[0]['dept']
          //判断是调用哪张表
          switch(this.value){
            case '新版员工考勤统计':{
              //获取数据，条件：时间，复核，审核
              var item = {}
              item["ID"] = this.number
              item["beginDay"] = this.begin
              item["endDay"] = this.end
              item["dept"] = dept
              if(this.roles[0] == 'Wage_Admin'){
                item["dept"] = this.selectdept
              }
              await GetStaticsAttend(item).then(res => {this.tableData=res})
              this.tableHeader=['工资号','姓名','发生年份','发生月份','请假类型','天数','补考勤扣款','补工作餐积点扣款','合计天数','合计补考勤扣款','合计补工作餐积点扣款']
              break
            }
            //repeat method
            case '新版劳务制员工考勤统计':{
               //获取数据，条件：时间，复核，审核
              var item = {}
              item["ID"] = this.number
              item["beginDay"] = this.begin
              item["endDay"] = this.end
              item["dept"] = dept
              if(this.roles[0] == 'Wage_Admin'){
                item["dept"] = this.selectdept
              }
              await GetWorkStaticsAttend(item).then(res => {this.tableData=res})
              this.tableHeader=['工资号','姓名','发生年份','发生月份','请假类型','天数','补考勤扣款','补工作餐积点扣款','合计天数','合计补考勤扣款','合计补工作餐积点扣款']
              break
            }
          }
          //this.tableData.push({部门:'合计：',工资号:'共计：7人',缺勤天数不含休息日:'321',补考勤扣款:'321',补工作餐积点扣款:'321'})
          var type = []
          this.tableData.forEach(i => {
            if(!type.includes(i['请假类型']))
            {type.push(i['请假类型'])}
          });
          var total = []
          type.forEach(i => {
            var day = 0
            var amount = 0
            var point = 0
            this.tableData.forEach(j =>{
              if(j['请假类型'] == i)
              {
                day = day + j['天数']
                amount = amount + j['补考勤扣款']
                point = point + j['补工作餐积点扣款']
              }
            })
            total.push({姓名:i,合计天数:day,合计补考勤扣款:amount,合计补工作餐积点扣款:point})
          })
          var day = 0
          var amount = 0
          var point = 0
          this.tableData.forEach(i =>{
            day = day + i['天数']
            amount = amount + i['补考勤扣款']
            point = point + i['补工作餐积点扣款']
          })
          total.push({姓名:'总计',合计天数:day,合计补考勤扣款:amount,合计补工作餐积点扣款:point})
          total.forEach(i =>{
            this.tableData.push(i)
          })

          this.$message({
            message: '查询成功！',
            type: 'success'
          })
        }
        else{
          this.flag = false
          this.$message({
          message: '查询失败！请填写合法且完整的信息！',
          type: 'error'
        })}
        this.loading = false    
      },
      close(){
        this.flag = false
      },
      handleDownload() {
          this.downloadLoading = true
          import('@/vendor/Export2Excel').then(excel => {
            const list = this.tableData
            const filterVal = this.tableHeader
            const data = this.formatJson(filterVal, list)
            excel.export_json_to_excel({
              header: this.tableHeader,
              data,
              filename: this.value,
              autoWidth: this.autoWidth,
              bookType: this.bookType
            })
            this.$message({
              message: '数据导出成功！',
              type: 'success'
            })
            this.downloadLoading = false
          })
        },
      formatJson(filterVal, jsonData) {
        return jsonData.map(v => filterVal.map(j => {
          if (j === 'timestamp') {
            return parseTime(v[j])
          } else {
            return v[j]
          }
        }))
      }
    }
  }
  </script>
  
  <style lang="css" scoped>
  .main{
    margin-top: 2%;
  }
  .time{
    padding-left:2%;
  }
  .el-select{
    margin-left: 2%;
  }
  .gap{
      padding-left:10%;
    }
  </style>