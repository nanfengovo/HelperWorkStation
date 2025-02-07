<template>
    <div class="Query conditions">
        <el-card class="Query-conditions-card">
            <div class="Query-conditions-card-title">
                <h4>查询条件</h4>
            </div>
            <div class="Query conditions-card-content">
                <el-form :inline="true" :model="queryForm" class="demo-form-inline">
                    <el-form-item label="日志内容关键字查询:">
                        <el-input  v-model="queryForm.operator" placeholder="请输入查询的关键字(支持模糊查询)" style="width: 250px;"></el-input>
                    </el-form-item>
                    <el-form-item label="日志级别:">
                        <el-select v-model="queryForm.level" placeholder="请选择日志级别" style="width: 180px;" clearable>
                            <el-option label="Trace" value="Trace"></el-option>
                            <el-option label="Info" value="Info"></el-option>
                            <el-option label="Error" value="Error"></el-option>
                            <el-option label="Warn" value="Warn"></el-option>
                            <el-option label="Debug" value="Debug"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="操作时间">
                        <el-date-picker
                        v-model="queryForm.date"
                        type="datetimerange"
                        range-separator="至"
                        start-placeholder="开始日期"
                        end-placeholder="结束日期"
                        />
                    </el-form-item>
                    <el-form-item class="Query-conditions-card-content-button">
                        <el-button type="primary" @click="query">查询</el-button>
                        <el-button @click="reset">重置</el-button>
                    </el-form-item>
                </el-form>
            </div>
        </el-card>
    </div>


        <el-card style="width: 100%" shadow="always" class="Logs-card" >
            <el-table :data="LogsData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="180" />
                <el-table-column prop="level" label="日志级别" width="120" />
                <el-table-column prop="logger" label="日志源" width="180" />
                <el-table-column prop="data" label="记录时间" />
                <el-table-column prop="message" label="日志内容" />
            </el-table>
            <!-- 分页控件 -->
            <el-pagination background layout="prev, pager, next" :total="100" />
        </el-card>
</template>
<script setup lang = "ts">
import axios from 'axios';
import { ElMessage } from 'element-plus';
import { computed, onMounted, ref } from 'vue';

// 查询条件 表单
const queryForm = ref<{
    operator: string;
    level: string;
    date: string;
}>({
    operator: "",
    level: "",
    date: "",
});

//表格数据
const LogsData = ref<Array<{id: number; level: string; logger: string; data: string; message: string }>>([]);


// 重置
const reset = () => {
    queryForm.value.operator = "";
    queryForm.value.level = "";
    queryForm.value.date = "";
    GetAllLogs();
};

// 获取日志数据
const GetAllLogs = async () => {
    try {
        const res = await axios.get('http://127.0.0.1:5264/api/OperationLogs/GetAllLogs', {
        params: {
        ...queryForm.value,
        startDate: queryForm.value.date ? queryForm.value.date[0] : null,
        endDate: queryForm.value.date ? queryForm.value.date[1] : null,
    },
    });
    LogsData.value = res.data;
    
} catch (error: any) {
        ElMessage({
        message: error.message,
        type: 'warning',
        });
    }
};



//初始化
onMounted(() => {
    GetAllLogs();
});



</script>
<style>
.Query-conditions-card{
    background-color: #F0FFF0;
    border-radius: 20px;
    display: flex;
}
.Query-conditions-card-title h4{
    text-align: center;
    font-weight: bold;
    margin-top: 10px;
}
.Query-conditions-card-content-button{
    margin-left: 1020px;
}
.Logs-card{
    margin-top: 20px;
}
</style>