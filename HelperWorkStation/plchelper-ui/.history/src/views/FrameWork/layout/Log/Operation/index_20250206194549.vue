<template>
    <div class="Query conditions">
        <el-card class="Query conditions-card">
            <div class="Query conditions-card-title">
                <h4>查询条件</h4>
            </div>
            <div class="Query conditions-card-content">
                <el-form :inline="true" :model="queryForm" class="demo-form-inline">
                    <el-form-item label="操作人员">
                        <el-input v-model="queryForm.operator" placeholder="操作人员"></el-input>
                    </el-form-item>
                    <el-form-item label="操作时间">
                        <el-date-picker
                            v-model="queryForm.date"
                            type="daterange"
                            range-separator="至"
                            start-placeholder="开始日期"
                            end-placeholder="结束日期"
                            value-format="yyyy-MM-dd"
                            align="right">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="query">查询</el-button>
                        <el-button @click="reset">重置</el-button>
                    </el-form-item>
                </el-form>
            </div>


        </el-card>
    </div>
</template>
<script setup lang = "ts">
import { ref } from 'vue';


const queryForm = ref("");
</script>
<style>
.Query conditions-card{
    background-color: #F0FFF0;
    height: 150px;
    border-radius: 20px;
}
.Query conditions-card-title h4{
    text-align: center;
    font-weight: bold;
    margin-top: 10px;
}
</style>