<template>
    <div class="Query">
        <el-card class="Query-card">
            <template #header>
                <div class="card-header">
                    <h4>根据条件查询</h4>
                </div>
                <el-row>
                    <el-label>
                        数据点名称:
                    </el-label>
                    <el-input v-model="dbName" placeholder="请输入数据点名称" clearable/>
                    <el-label>
                        备注:
                    </el-label>
                    <el-input v-model="remark" placeholder="请输入备注" clearable/>
                    <el-label>
                        数据点的值:
                    </el-label>
                    <el-input v-model="result" placeholder="请输入数据点的值" clearable/>
                </el-row>
                <el-row>
                    <el-label>
                        异常信息:
                    </el-label>
                    <el-input v-model="exceptionInfo" placeholder="请输入异常信息" clearable/>
                    <el-label>
                        日期
                    </el-label>
                    <el-date-picker v-model="dateTime" type="datetimerange" range-separator="至"
                    start-placeholder="开始日期" end-placeholder="结束日期" />
                </el-row>
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
.Query-card input{
    width: 240px;
}
</style>