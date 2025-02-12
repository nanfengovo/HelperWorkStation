<template>
    <div class="S7Card">
        <el-card class="S7config-card" shadow="hover">
            <div class="S7-header">
                <div class="card-top-top">
                        <h4>S7配置</h4>
                </div>
                <el-form :model="S7Config" class="S7Config-form">
                    <div class="content">
                        <el-form-item label = "S7配置名称:">
                            <el-input  v-model="S7Name" placeholder="请输入保存的配置名称" clearable  />
                        </el-form-item>
                        <el-form-item label = "IP:">
                            <el-input v-model="IP" placeholder="请输入IP地址" />
                        </el-form-item>
                        <el-form-item label = "端口号:">
                            <el-input v-model="Port" placeholder="请输入端口号" />
                        </el-form-item>
                        <el-form-item label = "PLC类型:">
                            <el-select  v-model="CPUType" placeholder="请选择CPU型号" style="width: 240px" clearable>
                                <el-option label="S7-1200" value="S7-1200" />
                                <el-option label="S7-1500" value="S7-1500" />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "Rack:">
                            <el-input v-model="Rack" placeholder="请输入Rack" />
                        </el-form-item>
                        <el-form-item label = "Slot:">
                            <el-input v-model="Slot" placeholder="请输入Slot" />
                        </el-form-item>
                    </div>
                    <div class="btn-group">
                        <el-button @click="cancel">取消此配置</el-button>
                        <el-button type="primary" @click = "AddS7Config">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>


        <el-card style="width: 100%" shadow="always" class="S7configData-card"> 
            <el-table :data="S7ConfigData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="60" />
                <el-table-column prop="s7Name" label="S7配置名称" width="180" />
                <el-table-column prop="ip" label="IP地址" width="180" />
                <el-table-column prop="port" label="端口" width="100" />
                <el-table-column prop="cpuType" label="CPU类型" width="180" />
                <el-table-column prop="rack" label="Rack" width="80" />
                <el-table-column prop="slot" label="Slot" width="80" />
                <el-table-column label="操作" width="300">
                    <template #default="scope">
                        <el-button type="primary" @click="Edit(scope.$index, scope.row)">修改</el-button>
                        <el-button type="danger" plain @click="Delete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>

        <!-- 插槽-编辑对话框 -->
        <el-dialog v-model="editFormVisible"  width="500" draggable >
            <slot-header class="dialog-title" name="dialog-title">
                <h3>修改S7配置</h3>
            </slot-header>
        <el-form :model="form">
                        <el-form-item label = "配置名称:">
                            <el-input v-model="form.s7Name" placeholder="请输入配置名称" clearable  />
                        </el-form-item>
                        <el-form-item label="IP地址:" >
                            <el-input v-model="form.ip" placeholder="请输入IP地址" clearable />
                        </el-form-item>
                        <el-form-item label = "端口:">
                            <el-input v-model="form.port" placeholder="请输入端口号" clearable />
                        </el-form-item>
                        <el-form-item label = "CPU类型:">
                            <el-select  v-model="form.cpuType" placeholder="请选择CPU型号" style="width: 240px" clearable>
                                <el-option label="S7-1200" value="S7-1200" />
                                <el-option label="S7-1500" value="S7-1500" />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "Rack:">
                            <el-input v-model="form.rack" placeholder="请输入Rack" clearable />
                        </el-form-item>
                        <el-form-item label = "Slot:">
                            <el-input v-model="form.slot" placeholder="请输入Slot" clearable />
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
import axios from 'axios';
import { ElMessage, ElMessageBox, ElNotification } from 'element-plus';
import { onMounted, ref } from 'vue';


const S7Name = ref('');
const IP = ref('');
const Port = ref(102);
const CPUType = ref('');
const Rack = ref('');
const Slot = ref('');

const editFormVisible = ref(false);
const form = ref<{id?:number, s7Name: string,ip: string,port:number,cpuType:string, rack:string, slot: string}>({id:0, s7Name: '',ip: '',port:102,cpuType:'', rack:'', slot: ''});

//添加
const AddS7Config = async () => {
    try {
        //使用正则表达式验证输入的是ip地址
    const ipPattern = /^(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$/;
    if (!ipPattern.test(IP.value)) {
        ElMessage({
        message: '请输入正确的IP地址',
        type: 'warning',
    })
        return;
    }    
    if (S7Name.value === '' || IP.value === '' || CPUType.value === '' || Rack.value === '' || Slot.value === '') {
            ElMessage({
                message: "请填写完整信息!",
                type: 'error',
            });
            return;
        }
        const S7Config = {
            S7Name: S7Name.value,
            IP: IP.value,
            Port: Port.value,
            CPUType: CPUType.value,
            Rack: Rack.value,
            Slot: Slot.value
        };
        // 调用接口，将S7Config保存到数据库
        const response = await axios.post('http://127.0.0.1:5264/api/S7Config/AddS7Config', S7Config, {
            headers: { 'Content-Type': 'application/json' },
        }); if(response.data.code === 200) {
                    ElNotification({
                        title: 'S7配置提示',
                        message: '新增S7配置成功!',
                        type: 'success',
                    });
                    S7Name.value = '';
                    IP.value = '';
                    Port.value = 102;
                    CPUType.value = '';
                    Rack.value = '';
                    Slot.value = '';
                    getS7ConfigData();
                } else {
                    ElNotification({
                        title: 'S7配置提示',
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
};


//取消
const cancel = () => {
    S7Name.value = '';
    IP.value = '';
    Port.value = 102;
    CPUType.value = '';
    Rack.value = '';
    Slot.value = '';
    ElMessage({
                message: "已清空!", 
                type: 'success',
            });
}
//定义表格数据
const S7ConfigData = ref<Array<{ id: number, s7Name :string,iP: string, port: number,cputype: string,rack: string,slot: string}>>([]);
//获取配置信息
async function getS7ConfigData() {
    try {
        const response = await axios.get('http://127.0.0.1:5264/api/S7Config/GetS7Confug');
        S7ConfigData.value = response.data; // 将响应数据赋值给 PLCData
    } catch (error: any) {
        ElMessage({
        message: "请检查后端服务是否启动",
        type: 'error',
    })
    }
}
// 在组件挂载时调用 getPLCData
onMounted(() => {
    getS7ConfigData();
});


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
        const response = await axios.delete(`http://127.0.0.1:5264/api/S7Config/DeleteS7Config?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            getS7ConfigData();
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


//修改配置
//编辑
//编辑
const Edit = (index: number, row: { id?:number, s7Name: string,ip: string,port:number,cpuType:string, rack:string, slot: string}) => {
    form.value = { ...row };
    editFormVisible.value = true;
}
//更新
const update = async () => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/S7Config/UpdateS7Config?id=${form.value.id}&S7Name=${form.value.s7Name}&IP=${form.value.ip}&Port=${form.value.port}&CPUType=${form.value.cpuType}&Rack=${form.value.rack}&Slot=${form.value.slot}`, {
            headers: { 'Content-Type': 'application/json' },
        });
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            getS7ConfigData();
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
</script>

<style scoped>
.S7Card {
    width: 100%;
}
.S7config-card {
    width: 100%;
    height: 280px;
    margin-top: 20px;
    margin-bottom: 40px;
    background-color: #F0FFF0;
    border-radius: 12px; 
}
.S7configData-card{
    border-radius: 12px; 
}
.S7Config-form .el-form-item {
    margin-bottom: 20px;
    margin-left: 20px;
    
}
.card-top-demo {
    display: flex;
    width: 100%;
}
.card-top-top {
    justify-content: center;
    display: flex;
}
h4 {
    align-items: center;
    justify-content: center;
    margin: 0 auto; /* 水平居中 */
}
.card-top-content{
    margin-top: 20px;
    margin-bottom: 20px;
}
.content-name>* {
    margin-left: 40px;
    float:left
}
.content-cputype>* {
    margin-left: 40px;
    float:left
}
.content-port>*{
    margin-left: 40px;
    
}
.content-rack>*{
    margin-top: 40px;
    margin-left: 40px;
    float:left
}
.content-slot>*{
    margin-top: 40px;
    margin-left: 40px;
    float:left
}
.btn-save{
    margin-top: 40px;
    margin-right: 140px;
    float:right
}
.btn-cancel{
    margin-top: 40px;
    margin-right: 40px;
    float:right
}
.dialog-title{
    width: 100%;
    height: 20px;
    text-align: center;
    background-color: aqua;
}

</style>