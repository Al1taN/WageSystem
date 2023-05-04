<template>
    <div>
        <h1 class="title">系统管理</h1>
        <br><br><br><br>
        <el-divider></el-divider>
        <div class="form01">
            <br><br>
            <span class="form02">导入截止年份：<input type="number" v-model="ddl01"></span>
            <span class="gap"></span>
            <span class="form02">导入截止月份：<input type="number" v-model="ddl02"></span>
            <span class="gap"></span>
            <span class="form02">导入截止日期：<input type="number" v-model="ddl03"></span>
            <br><br><br>
            <span class="form02">劳务制导入截止年份：<input type="number" v-model="ddl04"></span>
            <span class="gap"></span>
            <span class="form02">劳务制导入截止月份：<input type="number" v-model="ddl05"></span>
            <span class="gap"></span>
            <span class="form02">劳务制导入截止日期：<input type="number" v-model="ddl06"></span>
            <br><br><br>
            <span class="form02"><el-button @click="save">保存截止日期</el-button></span>
            <span class="form02"><el-button @click="check">查询是否超时</el-button></span>
            <br><br><br>
        </div> 
        <el-divider></el-divider>
        <div class="form01">       
            <br><br>
            <span class="form02"></span>
            <el-select v-model="value" placeholder="缺勤类型">
                <el-option
                v-for="item in options"
                :key="item.value"
                :label="item.label"
                :value="item.value">
                </el-option>
            </el-select>
            <span class="pad">备注：<el-input v-model="note"></el-input></span>
            <br><br><br>     
        </div> 
        <el-divider></el-divider>
        <br>
        <div class="form01"><span class="form02"><el-button @click="checkUser">点击查询当前用户</el-button></span></div>
        <br><br>
        <el-dialog title="用户列表" :visible.sync="dialogFormVisible4Edit">
            <div style=" font-size: 18px; ">
                添加用户数据：
                <span class="form02"></span>
                编号：<input type="text" class="input-user" v-model="pid" style ="width:150px;">
                <span class="form02"></span>
                <el-select v-model="dept" placeholder="部门对应的部门号">
                    <el-option
                    v-for="item in depts"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value">
                    </el-option>
                </el-select>
                <span class="pad"></span>
                部门号：<input type="text" class="input-user" v-model="dept" style ="width:150px;">
                <span class="form01"></span>
                <el-button @click="commit">提交</el-button>
            </div>
            <br><br>
            <el-table size="small" current-row-key="dept" :data="UserList" stripe highlight-current-row>
                <el-table-column type="index" align="center" label="序号"></el-table-column>
                <el-table-column prop="pid" label="编号" align="center"></el-table-column>
                <el-table-column prop="dept" label="部门号" align="center"></el-table-column>
                <el-table-column label="操作" align="center">
                    <template slot-scope="scope">
                        <el-button type="danger" size="mini" @click="handleDelete(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-dialog>
    </div>
  </template>
  
  <script>
    import { mapGetters } from 'vuex'
    import {UpdateDdltime,GetDdlTime,ImpDept,GetWageUser,DeleteWageUser} from '@/api/sysManage'
    export default {
        name: 'sysManage',
        //components: { FilenameOption, AutoWidthOption, BookTypeOption },
       created:async function(){ 
        await GetDdlTime().then(res=>{this.tableData=res})
        this.ddl01=this.tableData[0]["year"]
        this.ddl02 = this.tableData[0]["month"]
        this.ddl03 = this.tableData[0]["dateTime"]
        this.ddl04 = this.tableData[0]["workYear"]
        this.ddl05 = this.tableData[0]["workMonth"]
        this.ddl06 = this.tableData[0]["workDate"]
        }, 
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
                    value: '选项6',
                    label: '工伤假'
                    }, {
                    value: '选项7',
                    label: '怀孕假'
                    }, {
                    value: '选项8',
                    label: '婚假'
                    }, {
                    value: '选项9',
                    label: '计划生育假'
                    }, {
                    value: '选项10',
                    label: '经期假'
                    }, {
                    value: '选项11',
                    label: '旷工'
                    }, {
                    value: '选项12',
                    label: '留用查看'
                    }, {
                    value: '选项13',
                    label: '年休假'
                    }, {
                    value: '选项14',
                    label: '陪产假'
                    }, {
                    value: '选项15',
                    label: '全勤'
                    }, {
                    value: '选项16',
                    label: '人工流产假'
                    }, {
                    value: '选项17',
                    label: '丧假'
                    }, {
                    value: '选项18',
                    label: '事假'
                    }, {
                    value: '选项19',
                    label: '探亲假'
                    }, {
                    value: '选项20',
                    label: '停职检查'
                    }, {
                    value: '选项21',
                    label: '刑事强制措施'
                    }, {
                    value: '选项22',
                    label: '行政拘留'
                    }, {
                    value: '选项23',
                    label: '育儿假'
                    }, {
                    value: '选项24',
                    label: '育婴陪护假'
                    }, {
                    value: '选项25',
                    label: '重大疾病'
                    }, {
                    value: '选项26',
                    label: '自然流产假'           
                    }],

                depts: [{
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
                value: '',
                ddl01:'',
                ddl02:'',
                ddl03:'',
                ddl04:'',
                ddl05:'',
                ddl06:'',
                note:'',
                time:'',
                tableData:[{'year':1,'month':1,'datetime':1,'workyear':2,'workmonth':2,'workdate':2}],
                pid:'',
                dept:'',
                UserList:[],
                dialogFormVisible4Edit:false
            }  
        },
        computed: {
            ...mapGetters([
            'roles'
            ])
        },
        methods:{
            check(){
                var  date =  new  Date();
                var  year = date.getFullYear();
                var  month = date.getMonth() + 1 ;
                var  day = date.getDate();
                if  (month >= 1 && month <= 9) {
                    month =  "0"  + month;
                }
                if  (day >= 0 && day <= 9) {
                    day =  "0"  + day;
                }
                let time = year + '-' + month + '-' + day
                this.time = time
                this.$message({
                     message: this.time,
                     type: 'info'
                     })
                // if(this.time > this.ddl01 && this.time > this.ddl02)
                // {
                //     this.$message({
                //     message: '已超出所有截至时间！',
                //     type: 'warning'
                //     })
                // }
                // else if(this.time > this.ddl01)
                // {
                //     this.$message({
                //     message: '已超出导入截止日期的截至时间！',
                //     type: 'warning'
                //     })
                // }
                // else if(this.time > this.ddl02)
                // {
                //     this.$message({
                //     message: '已超出导入截止日期(劳务制)的截至时间！',
                //     type: 'warning'
                //     })
                // }
                // else{
                //     this.$message({
                //     message: '未超出截至时间！',
                //     type: 'success'
                //     })
                // }
            },
            save(){
                if(this.roles[0] == "Wage_Admin")
                {
                    const r = /^\+?[1-9][0-9]*$/
                    if(1900<= this.ddl01 && 1<=this.ddl02&&this.ddl02<=12 && 1<=this.ddl03 && this.ddl03<=31 && 1900<= this.ddl04 && 
                    1<=this.ddl05&&this.ddl05<=12 && 1<=this.ddl06 && this.ddl06<=31 
                    && r.test(this.ddl01) && r.test(this.ddl02) && r.test(this.ddl03)&& r.test(this.ddl04)&& r.test(this.ddl05)&& r.test(this.ddl06)){
                        UpdateDdltime(this.ddl01,this.ddl02,this.ddl03,this.ddl04,this.ddl05,this.ddl06).then(
                            this.$message({
                            message: '更新截止日期成功！',
                            type: 'success'
                            })
                        )
                    }
                    else{
                        this.$message({
                        message: '更新失败！请填写合法信息！',
                        type: 'error'
                        })
                    }
                }
                else{
                    this.$message({
                    message: '你没有更改截止日期的权限！',
                    type: 'error'
                    })
                }
            },
            commit(){
                if(this.roles[0] == "Wage_Admin")
                {
                    if(this.pid == '' || this.dept == ''){
                        this.$message({
                            message: '请输入合法信息！',
                            type: 'error'
                            })
                        return 0
                    }
                    var user = {}
                    user["pid"] = this.pid
                    user["dept"] = this.dept
                    ImpDept(user).then(res =>{
                        if(res == 200)
                        {
                            this.$message({
                            message: '提交用户信息成功！',
                            type: 'success'
                            })
                        }
                        if(res == 500)
                        {
                            this.$message({
                            message: '提交用户信息失败！',
                            type: 'error'
                            })
                        }
                    }).finally(async ()=>{
                        this.UserList=[]
                        this.pid = ''
                        this.dept = ''
                        //重新加载数据
                        await GetWageUser().then(res=>{this.UserList=res})
                    })
                }
                else{
                    this.$message({
                    message: '你没有录入用户信息的权限！',
                    type: 'error'
                    })
                }
            },
            async checkUser(){
                this.UserList=[]
                this.dialogFormVisible4Edit = true
                await GetWageUser().then(res=>{
                    this.UserList=res
                    console.log(this.UserList)
                  })
            },
            async handleDelete(row){
                this.$confirm("此操作将删除该用户，是否继续？","提示",{type:"info"}).then(async ()=>{
                    await DeleteWageUser(row.pid,row.dept).then((res)=>{
                            if(res == 200){this.$message.success("删除成功!")}
                            else{this.$message.success("删除失败!")}
                        }
                    ).finally(async ()=>{
                        this.UserList=[]
                        //重新加载数据
                        await GetWageUser().then(res=>{this.UserList=res})
                    })
                }).catch(()=>{
                    this.$message.info("取消操作")
                })
            },
        }
  }
  </script>
  
  <style lang="css" scoped>
.title{
    text-align: center;
}
.form01{
    text-align: left;
    margin-left: 10%;
    font-size: large;
  }
.el-button{
    font-size: 18px;
}
.el-table{
    font-size: 18px;
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
.input-user{
    width:5%;
}
.el-divider {
    background-color: #000000;
    height: 1px;
}
.block{
    padding-left:5%;
}
  </style>