<template>
    <div class="DBPointCard">
        <el-card class="DBPoint-config-card" shadow="hover">
            <div class="DBPoint-header">
                <div class="card-top-top">
                        <h4>DB块配置</h4>
                </div>
                <el-form :model="DBpointConfig" class="DBPointConfig-form">
                    <div class="content">
                        <el-form-item label = "S7配置名称:">
                            <el-select 
                            v-model="selectedS7Config" 
                            placeholder="请选择S7配置" 
                            style="width: 240px"
                            :loading="isLoading"
                            Clearable
                            >
                            <!-- 加载中显示提示 -->
                            <el-option v-if="isLoading" disabled label="加载中..." value="" />

                            <!-- 数据加载完成后的选项 -->
                            <el-option
                                v-for="item in PLCList"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "数据点名称:">
                            <el-input v-model="DBName" placeholder="数据点名称"  clearable/>
                        </el-form-item>
                        <el-form-item label = "数据类型:">
                            <el-input v-model="DBType" placeholder="请输入数据类型"  clearable/>
                        </el-form-item>
                        <el-form-item label = "DB块地址:">
                            <el-input  v-model="DBAddress" placeholder="请输入DB块地址"  clearable/>
                        </el-form-item>
                        <el-form-item label = "偏移量:">
                            <el-input v-model="DBOffset" placeholder="请输入偏移量" clearable />
                        </el-form-item>
                        <el-form-item label = "备注:">
                            <el-input v-model="Remark" placeholder="请输入备注" clearable />
                        </el-form-item>
                        <el-form-item label = "是否启用:">
                            <el-switch
                            v-model="IsOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                        />
                        </el-form-item>
                    </div>
                    <div class="btn-group">
                        <el-button @click="cancel">取消此配置</el-button>
                        <el-button type="primary" @click = "AddS7Config">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>


        <el-card style="width: 100%" shadow="always" class="DBPointconfigData-card"> 
            <el-table :data="DBPointConfigData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="60" />
                <el-table-column prop="DBName" label="S7数据点名称" width="180" />
                <el-table-column prop="S7Name" label="S7配置名称" width="180" />
                <el-table-column prop="DBType" label="数据类型" width="100" />
                <el-table-column prop="DBAddress" label="DB块地址" width="180" />
                <el-table-column prop="DBOffset" label="偏移量" width="80" />
                <el-table-column prop="Remark" label="备注" width="80" />

                <el-table-column prop="IsOpen" label="是否启用" width="80">
                    <template #default="scope">
                        <el-switch
                            v-model="scope.row.IsOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                        />
                    </template>
                </el-table-column>
                
                <el-table-column label="操作" width="300">
                    <template #default="scope">
                        <el-button type="primary" @click="Edit(scope.$index, scope.row)">修改</el-button>
                        <el-button type="danger" plain @click="Delete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>

        <!-- 插槽-编辑对话框 -->
        <!-- <el-dialog v-model="editFormVisible" title="修改S7配置" width="500" draggable >
        <el-form :model="form">
                        <el-form-item label = "配置名称:">
                            <el-input v-model="form.S7Name" placeholder="请输入配置名称" clearable  />
                        </el-form-item>
                        <el-form-item label="IP地址:" >
                            <el-input v-model="form.IP" placeholder="请输入IP地址" clearable />
                        </el-form-item>
                        <el-form-item label = "端口:">
                            <el-input v-model="form.Port" placeholder="请输入端口号" clearable />
                        </el-form-item>
                        <el-form-item label = "CPU类型:">
                            <el-select  v-model="form.CPUType" placeholder="请选择CPU型号" style="width: 240px" clearable>
                                <el-option label="S7-1200" value="S7-1200" />
                                <el-option label="S7-1500" value="S7-1500" />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "Rack:">
                            <el-input v-model="form.Rack" placeholder="请输入Rack" clearable />
                        </el-form-item>
                        <el-form-item label = "Slot:">
                            <el-input v-model="form.Slot" placeholder="请输入Slot" clearable />
                        </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
            <el-button @click="editFormVisible = false">取消</el-button>
            <el-button type="primary" @click="update">确认</el-button>
            </div>
        </template>
        </el-dialog> -->
    </div>
</template>
<script setup lang = "ts">


</script>
<style>
.DBPoint-config-card{
    width: 100%;
    margin-bottom: 20px;
    background-color: #F0FFF0;
    border-radius: 20px;
    
}
.DBPoint-config-card .el-card__body{
    padding: 20px;
}
.DBPoint-config-card .el-table{
    margin-top: 20px;
}
.DBPoint-config-card .el-table th{
    background-color: #E0FFFF;
}
.DBPoint-config-card .el-table td{
    background-color: #F0FFF0;
}
.DBPoint-config-card .el-table .el-button{
    margin-right: 10px;
}
.DBPoint-config-card .el-table .el-button--primary{
    background-color: #13ce66;
    border-color: #13ce66;
}
.DBPoint-config-card .el-input{
    width: 240px;
}
</style>