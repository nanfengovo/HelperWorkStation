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

    <!--数据展示-->
    <el-card class="data-card">
            <el-table-v2
        :columns="columns"
        :data="dbPointRWRecords"
        :sort-by="sortBy"
        :width=1300
        :height="350"
        class="table-v2"
        fixed
        @column-sort="onSort"
    />
    </el-card>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { TableV2FixedDir, TableV2SortOrder } from 'element-plus'

import type { SortBy } from 'element-plus'
import axios from 'axios'
import { id } from 'element-plus/es/locales.mjs'

// 定义列
const columns = [
    { key: 'id', title: 'ID', width: 80, sortable: true },
    { key: 'dbName', title: '数据点名称', width: 250, sortable: true },
    { key: 'remark', title: '备注', width: 400, sortable: true },
    { key: 'result', title: '结果', width: 150, sortable: true },
    { key: 'exceptionInfo', title: '异常信息', width: 200, sortable: true },
    { key: 'dateTime', title: '日期时间', width: 220, sortable: true },
]

// 定义表格数据
const dbPointRWRecords = ref<[id: number, dbName: string, remark: string, result: string, exceptionInfo: string, dateTime: string]>()

// 定义排序
const sortBy = ref({
    key: 'id',
    order: 'asc',
})

// 获取数据的方法
const fetchDBPointRWRecords = async () => {
    try {
    const response = await axios.get('http://localhost:5264/api/DBPointRWRecord/DBPointRWRecord')
    console.log(response.data)
    dbPointRWRecords.value = response.data
    } catch (error) {
    console.error('Error fetching DBPointRWRecords:', error)
    }
}

// 生命周期钩子，组件挂载后获取数据
onMounted(() => {
    fetchDBPointRWRecords()
})

// 排序事件处理
// const onSort = (newSortBy: SortBy) => {
//   // 实现排序逻辑或调用后端API进行排序
//   sortBy.value = newSortBy
// }
</script>
<style>
.Query{
    display: grid;
}
.Query-form .el-form-item{
    margin-left: 40px;
}
.Query-form .el-form-item .el-input{
    margin-left: 20px;
}
.Query-card{
    background-color: #F0FFF0;
    border-radius: 20px;
    height: 300px;
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
.row3{
    margin-left: 1000px;
}
.row3 button{
    margin-right: 50px;
    text-align: center;
}
.data-card{
    margin-top: 20px;
    border-radius: 20px;
    height: 400px;
}
</style>