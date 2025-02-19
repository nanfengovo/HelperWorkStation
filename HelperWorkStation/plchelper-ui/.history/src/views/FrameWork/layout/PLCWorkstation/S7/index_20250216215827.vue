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
    <el-card>
            <el-table-v2
        :columns="columns"
        :data="data"
        :sort-by="sortBy"
       
        fixed
        @column-sort="onSort"
    />
    </el-card>
</template>
<script setup lang="ts">
import { ref } from 'vue'
import { TableV2FixedDir, TableV2SortOrder } from 'element-plus'

import type { SortBy } from 'element-plus'

const generateColumns = (length = 10, prefix = 'column-', props?: any) =>
    Array.from({ length }).map((_, columnIndex) => ({
    ...props,
    key: `${prefix}${columnIndex}`,
    dataKey: `${prefix}${columnIndex}`,
    title: `Column ${columnIndex}`,
    width: 150,
}))

const generateData = (
    columns: ReturnType<typeof generateColumns>,
    length = 200,
    prefix = 'row-'
) =>
    Array.from({ length }).map((_, rowIndex) => {
    return columns.reduce(
        (rowData, column, columnIndex) => {
        rowData[column.dataKey] = `Row ${rowIndex} - Col ${columnIndex}`
        return rowData
    },
    {
        id: `${prefix}${rowIndex}`,
        parentId: null,
    }
    )
})

const columns = generateColumns(10)
let data = generateData(columns, 200)

columns[0].fixed = true
columns[1].fixed = TableV2FixedDir.LEFT
columns[9].fixed = TableV2FixedDir.RIGHT

for (let i = 0; i < 3; i++) columns[i].sortable = true

const sortBy = ref<SortBy>({
    key: 'column-0',
    order: TableV2SortOrder.ASC,
})

const onSort = (_sortBy: SortBy) => {
    data = data.reverse()
    sortBy.value = _sortBy
}
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
</style>