<template>
    <div>
        <h1 class="title">系统管理</h1>
        <br><br><br>
        <div class="form01">
            导入截止日期：<input type="date" v-model="ddl01">
            <br><br><br>
            导入截止日期(劳务制)：<input type="date" v-model="ddl02">
            <br><br><br>
        </div> 
        <el-divider></el-divider>
        <div class="form02">       
            <br><br><br>
            <el-select v-model="value" placeholder="缺勤类型">
                <el-option
                v-for="item in options"
                :key="item.value"
                :label="item.label"
                :value="item.value">
                </el-option>
            </el-select>
            <span class="pad">备注：<el-input v-model="note"></el-input></span>      
        </div>    
    </div>
  </template>
  
  <script>
    export default {
        name: 'hello',
        //components: { FilenameOption, AutoWidthOption, BookTypeOption },
        data(){
            return{
                options: [{
                    value: '选项1',
                    label: '病假'
                    }, {
                    value: '选项2',
                    label: '哺乳假'
                    }, {
                    value: '选项3',
                    label: '产假'
                    }, {
                    value: '选项4',
                    label: '产前假'
                    }, {
                    value: '选项5',
                    label: '父母护理假'
                    }, {
                    value: '选项5',
                    label: '工伤假'
                    }, {
                    value: '选项5',
                    label: '怀孕假'
                    }, {
                    value: '选项5',
                    label: '婚假'
                    }, {
                    value: '选项5',
                    label: '计划生育假'
                    }, {
                    value: '选项5',
                    label: '经期假'
                    }, {
                    value: '选项5',
                    label: '旷工'
                    }, {
                    value: '选项5',
                    label: '留用查看'
                    }, {
                    value: '选项5',
                    label: '年休假'
                    }, {
                    value: '选项5',
                    label: '陪产假'
                    }, {
                    value: '选项5',
                    label: '全勤'
                    }, {
                    value: '选项5',
                    label: '人工流产假'
                    }, {
                    value: '选项5',
                    label: '丧假'
                    }, {
                    value: '选项5',
                    label: '事假'
                    }, {
                    value: '选项5',
                    label: '探亲假'
                    }, {
                    value: '选项5',
                    label: '停职检查'
                    }, {
                    value: '选项5',
                    label: '刑事强制措施'
                    }, {
                    value: '选项5',
                    label: '行政拘留'
                    }, {
                    value: '选项5',
                    label: '育儿假'
                    }, {
                    value: '选项5',
                    label: '育婴陪护假'
                    }, {
                    value: '选项5',
                    label: '重大疾病'
                    }, {
                    value: '选项5',
                    label: '自然流产假'           
                    }],
                value: '',
                ddl01:'2022/9/15',
                ddl02:'2022/8/16',
                note:''
            }
        }
  }
  </script>
  
  <style lang="css" scoped>
.title{
    text-align: center;
}
.form01{
    text-align: right;
    margin-right: 80%;
  }
.form02{
    margin-left: 5%;
  }
.pad{
    margin-left: 1%;
}
.el-input {
        width: 250px;
      }
.el-divider {
    background-color: #000000;
    height: 1px;
}
  </style>