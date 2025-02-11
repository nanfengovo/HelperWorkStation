<template>
    <div class="modbus">
        <el-card class="modbusconfig-card" shadow="hover">
            <div class="modbus-header">
                <div class="card-top-top">
                        <h4>Modbus配置</h4>
                </div>
                <el-form :model="modbusConfig" class="modbusConfig-form">
                    <div class="content">
                        <el-form-item label = "选择PLC:">
                            <el-select 
                            v-model="selectedPLC" 
                            placeholder="请选择PLC" 
                            style="width: 240px"
                            :loading="isLoading"
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
                        <el-form-item label = "设备ID:">
                            <el-input-number v-model="slaveId" :min="1"   />
                        </el-form-item>
                        <el-form-item label="Function code" class="function-code" >
                                <el-select
                                    v-model="functionCodes"
                                    style="width: 240px;"
                                    placeholder="请选择功能码"
                                    clearable
                                >
                                    <el-option label="01 Read Coils（0x）" value="01 Read Coils（0x）" />
                                    <el-option value="02 Read Discrete Inputs (1x)" />
                                    <el-option value="03 Read Holding Registers (4x)" />
                                    <el-option value="04 Read Input Registers (3x)" />
                                    <el-option value="05 Write Single Coil" />
                                    <el-option value="06 Write Single Register" />
                                    <el-option value="15 Write Multiple Coils" />
                                    <el-option value="16 WriteMultipleRegisters" />
                                </el-select>
                        </el-form-item>
                        <el-form-item label="起始地址:" >
                            <el-input v-model="address" placeholder="请输入地址" />
                        </el-form-item>
                        <el-form-item label = "数量:">
                            <el-input-number v-model="num" :min="1" :max="32"  />
                        </el-form-item>
                        <el-form-item label = "配置名称:">
                            <el-input v-model="ModbusName" placeholder="请输入保存的配置名称" />
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
                        <el-button type="primary" @click = "AddMobusConfig">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>


        <el-card style="width: 100%" shadow="always" class="Modbus-card" >
            <el-table :data="ModbusData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="180" />
                <el-table-column prop="modbusName" label="Modbus配置的名称" width="180" />
                <el-table-column prop="plcName" label="PLC名称" width="120" />
                <el-table-column prop="slaveId" label="设备ID" />
                <el-table-column prop="functionCode" label="功能码" width = "180" />
                <el-table-column prop="startAddress" label="起始地址" />
                <el-table-column prop="num" label="数量" />
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
                <el-table-column label="操作" width="400">
                    <template #default="scope">
                        <el-button type="success" @click="Enable(scope.$index, scope.row)">启用</el-button>
                        <el-button type="info" @click="WatchStatus(scope.$index, scope.row)">监控状态</el-button>
                        <el-button type="warning" @click="Disable(scope.$index, scope.row)">停用</el-button>
                        <el-button type="primary" @click="Edit(scope.$index, scope.row)">修改</el-button>
                        <el-button type="danger" plain @click="Delete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>


        <!-- 插槽-编辑对话框 -->
        <el-dialog v-model="editFormVisible" title="修改Modbus配置" width="500" draggable >
        <el-form :model="form">
            <el-form-item label = "选择PLC:">
                            <el-select 
                            v-model="form.plcName" 
                            placeholder="请选择PLC" 
                            style="width: 240px"
                            :loading="isLoading"
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
                        <el-form-item label = "设备ID:">
                            <el-input-number v-model="form.slaveId" :min="1"   />
                        </el-form-item>
                        <el-form-item label="Function code" class="function-code" >
                                <el-select
                                    v-model="form.functionCode"
                                    style="width: 240px;"
                                    placeholder="请选择功能码"
                                    clearable
                                >
                                    <el-option label="01 Read Coils（0x）" value="01 Read Coils（0x）" />
                                    <el-option value="02 Read Discrete Inputs (1x)" />
                                    <el-option value="03 Read Holding Registers (4x)" />
                                    <el-option value="04 Read Input Registers (3x)" />
                                    <el-option value="05 Write Single Coil" />
                                    <el-option value="06 Write Single Register" />
                                    <el-option value="15 Write Multiple Coils" />
                                    <el-option value="16 WriteMultipleRegisters" />
                                </el-select>
                        </el-form-item>
                        <el-form-item label="起始地址:" >
                            <el-input v-model="form.startAddress" placeholder="请输入地址" />
                        </el-form-item>
                        <el-form-item label = "数量:">
                            <el-input-number v-model="form.num" :min="1" :max="32"  />
                        </el-form-item>
                        <el-form-item label = "配置名称:">
                            <el-input v-model="form.modbusName" placeholder="请输入保存的配置名称" />
                        </el-form-item>
                        <el-form-item label = "是否启用:">
                            <el-switch
                            v-model="form.isOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                        />
                        </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
            <el-button @click="editFormVisible = false">取消</el-button>
            <el-button type="primary" @click="update">确认</el-button>
            </div>
        </template>
        </el-dialog>

        
    </div>
</template>
<script setup lang = "ts">
import { Watch } from '@element-plus/icons-vue';
import axios from 'axios';
import { ElMessage, ElMessageBox, ElNotification } from 'element-plus';
import { onMounted, reactive, ref } from 'vue';

const editFormVisible = ref(false);
const form = ref<{id?:number, plcName: string,slaveId: number,functionCode:string,startAddress:string, num:number, modbusName: string,isOpen:boolean}>({id:0, plcName: '',slaveId: 1,functionCode:'',startAddress:'', num:1, modbusName: '',isOpen: true});

//     functionCodes: '',
//     address: ''
// });


const num = ref(1);
const slaveId = ref(1);
const functionCodes = ref('');
const address = ref('');
const quantity = ref('');
const ModbusName = ref('');
const selectedPLC = ref(''); // 用于存储用户选择的值
const PLCList = ref([]);       // 存储格式化后的选项
const isLoading = ref(true);   // 加载状态

// interface ModbusData {
//     id: number;
//     mobusName: string;
//     plcName: string;
//     slaveId: number;
//     functionCode: string;
//     StartAddress: string;
//     num: number;
// }

const ModbusData = ref<Array<{ id: number, modbusName: string, plcName: string,slaveId: number, functionCode: string,StartAddress: string,num: number,isOpen: boolean }>>([]);

//下拉框数据
const GetAllPLCConfig = async () => {
    try{
        const response = await axios.get("http://127.0.0.1:5264/api/ModbusConfig/GetPLCConfig");
    
    // 将字符串数组转换为 { label: string, value: string } 格式
    PLCList.value = response.data.map((item: any) => ({
      label: item,   // 显示名称
      value: item    // 绑定值（与 label 相同）
    }));

    isLoading.value = false;
    }
    
    catch (error) {
        ElMessage({
            message: "获取PLC列表失败,请检查后端是否启动!", 
            type: 'error',
        });
    isLoading.value = false;
    }
    // console.log(response.data);
    // PLCList = response.data;
    // console.log(PLCList);
}
//加载下拉框数据
onMounted(() => {
    GetModbusData();
    GetAllPLCConfig();
});

//取消配置
const cancel = () => {
    if(selectedPLC.value === '' && functionCodes.value === '' && address.value === '' && quantity.value === '' && ModbusName.value === ''){
        ElMessage({
            message: "表单为空,无需清空!", 
            type: 'warning',
        });
        return;
    }
    ElMessageBox.confirm(
    '继续操作将删除该配置,是否继续?',
    'Warning',
    {
        confirmButtonText: '继续此操作将会清空表单',
        cancelButtonText: '取消',
        type: 'warning',
    }
    )
.then(() => {
    // 清空表单
    
    functionCodes.value = '';
    slaveId.value = 1;
    address.value = '';
    quantity.value = '';
    selectedPLC.value = '';
    num.value = 1;
    ModbusName.value = '';
    ElMessage({
                message: "已清空!", 
                type: 'success',
            });
});
}

//表格数据--ModbusData
const GetModbusData = async () => {
    try{
        // const ModbusData = ref<ModbusData[]>([]);
        const response = await axios.get("http://127.0.0.1:5264/api/ModbusConfig/GetModbusConfug");
        console.log(response.data);
        ModbusData.value = response.data;
        console.log(ModbusData.value);
    }
    catch (error) {
    console.error('获取Modbus配置失败:', error);
    }
}


//新增配置
const AddMobusConfig = async () => {
            try{
                if(ModbusName.value === '' || selectedPLC.value === ''  || functionCodes.value === '' || address.value === '' ){
                    ElNotification({
                        title: 'Modbus配置提示',
                        message: '请填写完整的Modbus配置!',
                        type: 'warning',
                    });
                    return;
                }
                const modbusConfig = {
                    modbusName: ModbusName.value,
                    plcName: selectedPLC.value,
                    slaveId: slaveId.value,
                    functionCode: functionCodes.value,
                    startAddress: address.value,
                    num: num.value,
                    isOpen: true
                };
                const response = await axios.post("http://127.0.0.1:5264/api/ModbusConfig/AddModbusConfug",modbusConfig,{
                    headers: { 'Content-Type': 'application/json' },
                });
                if(response.data.code === 200) {
                    ElNotification({
                        title: 'Modbus配置提示',
                        message: '新增Modbus配置成功!',
                        type: 'success',
                    });
                    GetModbusData();
                } else {
                    ElNotification({
                        title: 'Modbus配置提示',
                        message: response.data.message,
                        type: 'warning',
                    });
                }
            }catch (error:any) {
                ElNotification({
                        title: '异常提示',
                        message: error.message,
                        type: 'error',
                    });
            }
        }

//修改配置
//编辑
//编辑
const Edit = (index: number, row: { id: number, plcName: string,slaveId: number,functionCode:string,startAddress:string, num:number, modbusName: string}) => {
    form.value = { ...row };
    editFormVisible.value = true;
}
//更新
const update = async () => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/ModbusConfig/UpdateModbusConfig?Id=${form.value.id}&PLCName=${form.value.plcName}&SlaveId=${form.value.slaveId}&FunctionCode=${form.value.functionCode}&StartAddr=${form.value.startAddress}&Num=${form.value.num}&ConfigName=${form.value.modbusName}`, {
            headers: { 'Content-Type': 'application/json' },
        });
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            GetModbusData();
            editFormVisible.value = false;
        } else {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'warning',
            });
        }
    } catch (error: any) {
        ElMessage({
            message: error.message,
            type: 'error',
        });
    }
}


//删除
const Delete = async (index: number, row: { id: number }) => {
    ElMessageBox.confirm(
    '继续操作将删除该配置,是否继续?',
    'Warning',
    {
        confirmButtonText: '确认删除',
        cancelButtonText: '取消',
        type: 'warning',
    }
)
    .then(async () => {
        try {
        const response = await axios.delete(`http://127.0.0.1:5264/api/ModbusConfig/DeleteModbusConfig?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            GetModbusData();
        } else {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'warning',
            });
        }
    } catch (error: any) {
        ElMessage({
            message: error.message,
            type: 'error',
        });
    }
    })
    .catch(() => {
        ElMessage({
        type: 'info',
        message: '取消删除',
        })
    })
    
}

//启用
const Enable = async (index: number, row: { id: number }) => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/ModbusConfig/EnableModbusConfig?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            GetModbusData();
        } else if (response.data.code == 401) {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'warning',
            });
        }
        else {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'error',
            });
        }
    } catch (error: any) {
        ElMessage({
            message: error.message,
            type: 'error',
        });
    }
}


//停用
const Disable = async (index: number, row: { id: number }) => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/ModbusConfig/DisableModbusConfig?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            GetModbusData();
        } else if (response.data.code == 401) {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'warning',
            });
        }
        else {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'error',
            });
        }
    } catch (error: any) {
        ElMessage({
            message: error.message,
            type: 'error',
        });
    }
}

</script>

<style >
.content{
    display: flex;
    margin: 20px;
    flex-wrap: wrap;
}
.modbusConfig-form .el-form-item{
    margin-right: 20px;
}
.btn-group{
    display: flex;
    justify-content: right;
    margin-right: 40px;
    margin-top: 50px;
}
.function-code>.el-select{
    width: 240px;
}
.modbusconfig-card{
    width: 100%;
    height: 300px;
    background-color: #F0FFF0;
    border-radius: 20px;
}
.card-top-top h4{
    font-size: 20px;
    font-weight: bold;
    text-align: center;
}
.content-plc{
    margin-left: 40px;
}
.demo-form-inline .el-select {
    --el-select-width: 220px;
}
.Modbus-card{
    margin-top: 40px;
    border-radius: 15px;
}

</style>