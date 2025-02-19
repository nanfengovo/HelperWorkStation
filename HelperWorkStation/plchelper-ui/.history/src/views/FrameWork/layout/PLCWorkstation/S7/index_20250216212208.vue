<template>
    <div class="Query">
        <el-card class="Query-card">
            <template #header>
                <div class="card-header">
                    <h4>根据条件查询数据点的读写记录</h4>
                </div>
                <el-form class="Query-form">
                    <el-row class="row1">
                        <el-form-item>
                            <el-label>
                                数据点名称:
                            </el-label>
                            <el-input v-model="dbName" placeholder="请输入数据点名称" clearable />
                        </el-form-item>
                        <el-form-item>
                            <el-label>
                                备注:
                            </el-label>
                            <el-input v-model="remark" placeholder="请输入备注" clearable/>
                        </el-form-item>
                        <el-form-item>
                            <el-label>
                                数据点的值:
                            </el-label>
                            <el-input v-model="result" placeholder="请输入数据点的值" clearable/>
                        </el-form-item>
                    </el-row>
                    <el-row class="row2">
                        <el-form-item>
                            <el-label>
                                &nbsp;&nbsp;&nbsp;异常信息:
                            </el-label>
                            <el-input v-model="exceptionInfo" placeholder="请输入异常信息" clearable/>
                        </el-form-item>
                        <el-form-item>
                            <el-label>
                                日期
                            </el-label>
                            <el-date-picker
                                v-model="value1"
                                type="datetime"
                                placeholder="选择时间"
                                format="YYYY-MM-DD HH:mm:ss"
                                date-format="MMM DD, YYYY"
                                time-format="HH:mm"
                            />
                        </el-form-item>
                    </el-row>
                    <el-row class="row3">
                        <el-form-item>
                            <el-button  @click="cancel">取消</el-button>
                        </el-form-item>
                        <el-form-item>
                            <el-button type="primary" @click="onSubmit">查询</el-button>
                        </el-form-item>
                    </el-row>
                </el-form>
            </template>
            
        </el-card>
    </div>
</template>
<script setup lang="ts">

</script>
<style>
.Query{
    display: grid;
}
.Query-card{
    background-color: #F0FFF0;
    border-radius: 20px;
}
.card-header{
    text-align: center;
}
.Query-card .el-input{
    width: 240px;
}
.row2{
    margin-top: 10px;
}
.row3 button{
    margin-right: 10px;
    text-align: center;
}
</style>