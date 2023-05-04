<template>
    <div>
      <el-select v-model="value" placeholder="请选择导入数据类别">
        <el-option
          v-for="item in options"
          :key="item.value"
          :label="item.label"
          :value="item.value">
        </el-option>
      </el-select>
      <el-button @click="save">保存</el-button>
      <div class="app-container">
        <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
        <el-table :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
          <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" />
        </el-table>
      </div>
    </div>
  </template>
  
  <script>
  import UploadExcelComponent from '@/components/UploadExcel/index.vue'
  export default {
    name: 'dataImport',
    components: { UploadExcelComponent },
    data() {
      return {
        options: [{
          value: '1',
          label: '新版员工考勤'
          }, {
            value: '2',
            label: '新版早夜班加班'
          }, {
            value: '3',
            label: '劳务制(新版)员工考勤'
          }, {
            value: '4',
            label: '劳务制(新版)早夜班加班'           
          }],
          value: '',
          tableData: [],
          tableHeader: []
        } 
  },
    methods: {
      beforeUpload(file) {
        const isLt1M = file.size / 1024 / 1024 < 1

        if (isLt1M) {
          return true
        }

        this.$message({
          message: '文件大小不能超过1M.',
          type: 'warning'
        })
        return false
      },
      handleSuccess({ results, header }) {
        this.tableData = results
        this.tableHeader = header
      },
      save(){
        this.$message({
          message: '保存成功！',
          type: 'success'
        })
      }
    }
  }
  </script>
  
  <style lang="css" scoped>
  .el-select{
    margin-left: 20%;
    margin-top: 2%;
    position: fixed;
  }
  .el-button{
    margin-left: 20%;
    margin-top: 5%;
    position: fixed;
  }
  </style>