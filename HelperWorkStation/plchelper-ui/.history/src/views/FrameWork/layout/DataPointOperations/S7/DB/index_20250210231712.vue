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
                                v-for="item in selectedS7ConfigList"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "数据点名称:">
                            <el-input v-model="dbName" placeholder="数据点名称"  clearable/>
                        </el-form-item>
                        <el-form-item label = "数据类型:">
                            <el-input v-model="dbType" placeholder="请输入数据类型"  clearable/>
                        </el-form-item>
                        <el-form-item label = "DB块地址:">
                            <el-input  v-model="dbAddress" placeholder="请输入DB块地址"  clearable/>
                        </el-form-item>
                        <el-form-item label = "偏移量:">
                            <el-input v-model="dbOffset" placeholder="请输入偏移量" clearable />
                        </el-form-item>
                        <el-form-item label = "备注:">
                            <el-input v-model="remark" placeholder="请输入备注" clearable />
                        </el-form-item>
                        <el-form-item label = "是否启用:">
                            <el-switch
                            v-model="isOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                        />
                        </el-form-item>
                    </div>
                    <div class="btn-group">
                        <el-button @click="cancel">取消此配置</el-button>
                        <el-button type="primary" @click = "AddDBPointConfig">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>


        <el-card style="width: 100%" shadow="always" class="DBPointconfigData-card"> 
            <el-table :data="DBPointConfigData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="60" />
                <el-table-column prop="dbName" label="S7数据点名称" width="180" />
                <el-table-column prop="s7Name" label="S7配置名称" width="180" />
                <el-table-column prop="dbType" label="数据类型" width="100" />
                <el-table-column prop="dbAddress" label="DB块地址" width="180" />
                <el-table-column prop="dbOffset" label="偏移量" width="80" />
                <el-table-column prop="remark" label="备注" width="80" />

                <el-table-column prop="isOpen" label="是否启用" width="80">
                    <template #default="scope">
                        <el-switch
                            v-model="scope.row.isOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                            disabled
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
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
import { onMounted, ref } from 'vue';


const DBpointConfig = ref({
    dbName: '',
    dbType: '',
    dbAddress: '',
    dbOffset: '',
    remark: '',
    isOpen: true
}); //DB配置数据
const selectedS7Config = ref(''); //存用户选择的S7配置名称
const isLoading = ref(true); //加载状态
const dbName = ref(''); //数据点名称
const dbType = ref(''); //数据类型
const dbAddress = ref(''); //DB块地址
const dbOffset = ref(''); //偏移量
const remark = ref(''); //备注
const isOpen = ref(true); //是否启用

const selectedS7ConfigList = ref([]); //S7配置列表

//s7配置名称下拉框数据
async function GetAllS7Config() {
    try {
        const res = await axios.get('http://127.0.0.1:5264/api/DBPointConfig/GetAllS7');
         // 将字符串数组转换为 { label: string, value: string } 格式
        selectedS7ConfigList.value = res.data.map((item: any) => ({
      label: item,   // 显示名称
      value: item    // 绑定值（与 label 相同）
    }));
        isLoading.value = false; // 加载完成
    } catch (error) {
        ElMessage({
            message: "获取S7列表失败,请检查后端是否启动!", 
            type: 'error',
        });
    isLoading.value = false;
    }
}

const DBPointConfigData = ref<Array<{id: number,DBName: string ,S7Name: string, DBType: string, DBAddress: string, DBOffset: string, Remark: string, IsOpen: boolean}>>([]); //DB配置数据

//获取DB配置数据
async function GetDBPointConfig() {
    try {
        const res = await axios.get('http://localhost:5264/api/DBPointConfig/GetAllDBPointConfig');
        DBPointConfigData.value = res.data;
    } catch (error) {
        ElMessage({
            message: "获取DB配置数据失败,请检查后端是否启动!", 
            type: 'error',
        });
    }
    }

//cancel按钮点击事件
function clear() {
    selectedS7Config.value = '';
    dbName.value = '';
    dbType.value = '';
    dbAddress.value = '';
    dbOffset.value = '';
    remark.value = '';
    isOpen.value = true;
}

const cancel = () => {
    ElMessageBox.confirm(
    '确定要取消保存此配置?',
    '警告',
    {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
    }
    )
    .then(() => {
        clear(); //清空数据
        ElMessage({
        type: 'success',
        message: '清除成功!',
        })
    })
    .catch(() => {
        ElMessage({
            type: 'info',
            message: '取消清除',
        })
    })
}

//AddDBPointConfig按钮点击事件
const AddDBPointConfig = () => {
    ElMessageBox.confirm(
    '确定要保存此配置?',
    '警告',
    {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
    }
    )
    .then(() => {
        //发送请求
        axios.post('http://localhost:5264/api/DBPointConfig/AddDBPointConfig', {
            S7Name: selectedS7Config.value,
            DBName: dbName.value,
            DBType: dbType.value,
            DBAddress: dbAddress.value,
            DBOffset: dbOffset.value,
            Remark: remark.value,
            IsOpen: isOpen.value
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then((res) => {
            console.log(res.data);
            if (res.data == 200) {
                ElMessage({
                    message: "添加成功!",
                    type: 'success',
                });
                clear(); //清空数据
                GetDBPointConfig(); //刷新数据
            } 
            else {
                ElMessage({
                    message: res.data,
                    type: 'error',
                });
            }
        })
        .catch((error) => {
            ElMessage({
                message: "添加失败,请检查数据是否正确!",
                type: 'error',
            });
        })
    })
    .catch(() => {
        ElMessage({
            type: 'info',
            message: '取消保存',
        })
    })
}



//组合挂载时加载数据
onMounted(() => {
    //加载S7配置数据
    GetAllS7Config();
    //加载DB配置数据
    GetDBPointConfig();
});
</script>
<style>
.DBPoint-config-card{
    width: 100%;
    margin-bottom: 20px;
    background-color: #F0FFF0;
    border-radius: 20px;
    
}
.DBPoint-config-card .el-form-item{
    margin-left: 20px;
}

.DBPoint-config-card .el-input{
    width: 240px;
}
</style>