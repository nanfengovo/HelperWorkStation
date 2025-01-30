<template>
    <div class="PLCCard">
        <el-card class="card-top" shadow="hover">
                    <div class="card-top-top">
                        <h4>PLC配置</h4>
                    </div>
                    <div class="card-top-content">
                        <div class="content-name">
                            <label>PLC名称:</label>
                            <el-input
                                v-model="inputName"
                                style="width: 240px"
                                placeholder="请输入PLC的名称"
                                clearable
                            />
                        </div>
                        <div class="content-ip">
                            <label>ip:</label>
                            <el-input
                                v-model="inputIP"
                                style="width: 240px"
                                placeholder="请输入PLC的ip"
                                clearable
                            />
                        </div>
                        <div class="content-port">
                            <label>端口:</label>
                            <el-input
                                v-model="inputPort"
                                style="width: 240px"
                                placeholder="请输入PLC的端口"
                                clearable
                            />
                        </div>
                        <div class="btn-operate">
                            <div class="btn-save" v-on:click="save">
                                <el-button type="primary" >保存</el-button>
                            </div>
                            <div class="btn-cancel" v-on:click="cancel">
                                <el-button >取消</el-button>
                            </div>
                        </div>
                    </div>
        </el-card>
        <el-card style="width: 100%" shadow="always" >
            <el-table :data="PLCData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="180" />
                <el-table-column prop="ip" label="IP地址" />
                <el-table-column prop="port" label="端口" width="400" />
                <el-table-column label="操作" width="300">
                    <template #default="scope">
                        <el-button type="primary" @click="Edit(scope.$index, scope.row)">修改</el-button>
                        <el-button type="danger" plain @click="Delete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>

        <!-- 插槽-编辑对话框 -->
        <el-dialog v-model="editFormVisible" title="修改PLC配置" width="500" >
        <el-form :model="form">
            <el-form-item label="IP地址" :label-width="formLabelWidth">
            <el-input v-model="form.ip" autocomplete="off" />
            </el-form-item>
            <el-form-item label="端口" :label-width="formLabelWidth">
            <el-input v-model="form.port" autocomplete="off" />
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
import { ElMessage, ElMessageBox } from 'element-plus';
import { onMounted, ref } from 'vue';
import axios from 'axios';
// import { savePLCConfig } from '@/api/PLCConfig';
//pLc配置

const editFormVisible = ref(false);
const form = ref<{ id?: number, ip: string, port: number }>({ ip: '', port: 0 });
const formLabelWidth = '120px';


interface PLC {
    inputIP: string;
    inputPort: number;
    inputName: string;//PLC名称
}

const inputIP = ref('');
const inputPort = ref('');
const inputName = ref('');//PLC名称



//取消
const cancel = () => {
    if(inputIP.value === '' && inputPort.value === ''){
        ElMessage({
        message: '还没填写任何内容',
        type: 'info',
    })
        return;
    }
    inputIP.value = '';
    inputPort.value = '';
    ElMessage({
        message: '已清空',
        type: 'success',
    })
}

const PLCData = ref<Array<{ id: number, iP: string, port: number }>>([]);

//保存
const save = async () => {
    //使用正则表达式验证输入的是ip地址
    const ipPattern = /^(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$/;
    if (inputIP.value === '') {
        ElMessage({
        message: '请输入IP地址',
        type: 'warning',
    })
        return;
    }
    if (!ipPattern.test(inputIP.value)) {
        ElMessage({
        message: '请输入正确的IP地址',
        type: 'warning',
    })
        return;
    }
    if (inputPort.value === '') {
        ElMessage({
        message: '请输入端口号',
        type: 'warning',
    })
        return;
    }
    // 定义 PLC 配置对象
const plcConfig = {
    IP: inputIP.value, // PLC 的 IP 地址
    Port: parseInt(inputPort.value),       // PLC 的端口号
};
    try {
        const response = await axios.post('http://127.0.0.1:5264/api/PLCConfig/AddPLCConfig',plcConfig,{
            headers: { 'Content-Type': 'application/json' },
        }
    );
    getPLCData();
        //根据服务器返回的状态码判断是否保存成功
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
        }else{
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
    })
    }
}


//PLCData
async function getPLCData() {
    try {
        const response = await axios.get('http://127.0.0.1:5264/api/PLCConfig/GetAllPLC');
        console.log(response.data);
        PLCData.value = response.data; // 将响应数据赋值给 PLCData
    } catch (error: any) {
        ElMessage({
        message: "请检查后端服务是否启动",
        type: 'error',
    })
    }
}
// 在组件挂载时调用 getPLCData
onMounted(() => {
    getPLCData();
});


//编辑
const Edit = (index: number, row: { id: number, ip: string, port: number }) => {
    form.value = { ...row };
    editFormVisible.value = true;
}

//更新
const update = async () => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/PLCConfig/UpdatePLCConfig?id=${form.value.id}&ip=${form.value.ip}&port=${form.value.port}`, {
            headers: { 'Content-Type': 'application/json' },
        });
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            getPLCData();
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
        const response = await axios.delete(`http://127.0.0.1:5264/api/PLCConfig/DeletePLCConfig?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            getPLCData();
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
</script>

<style scoped>
.PLCCard {
    width: 100%;
}
.card-top {
    width: 100%;
    height: 180px;
    margin-top: 20px;
    margin-bottom: 40px;
    background-color: #f5fffa; /* 半透明背景 */
    backdrop-filter: blur(10px); /* 毛玻璃效果 */
    -webkit-backdrop-filter: blur(10px); /* 兼容 Safari */
    border-radius: 12px;
    border: 1px solid rgba(50, 145, 150, 0.473); /* 边框 */ 
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
.content-name>*{
    margin-left: 40px;
    float:left
}
.content-ip>* {
    margin-left: 40px;
    float:left
}
.content-cputype>* {
    margin-left: 40px;
    float:left
}
.content-port>*{
    margin-left: 40px;
    float:left
}
.content-rack>*{
    margin-left: 40px;
    float:left
}
.content-slot>*{
    margin-top: 40px;
    margin-left: 40px;
    float:left
}
/* .btn-save{
    margin-bottom: 20px;
    margin-right: 140px;
    float:right
}
.btn-cancel{
    margin-bottom: 20px;
    margin-right: 40px;
    float:right
} */
.btn-operate{
    margin-top: 20px;
    margin-right: 40px;
    float:right
}
</style>