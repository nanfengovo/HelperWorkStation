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
                            <el-input v-model="S7Name" placeholder="请输入保存的配置名称" />
                        </el-form-item>
                        <el-form-item label = "IP:">
                            <el-input v-model="IP" placeholder="请输入IP地址" />
                        </el-form-item>
                        <el-form-item label = "端口号:">
                            <el-input v-model="port" placeholder="请输入端口号" />
                        </el-form-item>
                        <el-form-item label = "PLC类型:">
                            <el-select  v-model="selectCpu" placeholder="请选择CPU型号" style="width: 240px" clearable>
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
        <el-card style="width: 100%" shadow="always">显示已经配置好的保存到数据库的数据</el-card>
    </div>
</template>
<script setup lang = "ts">
import axios from 'axios';
import { ElMessage } from 'element-plus';
import { ref } from 'vue';


const S7Name = ref('');
const IP = ref('');
const port = ref(102);
const selectCpu = ref('');
const Rack = ref('');
const Slot = ref('');

//添加
const AddS7Config = async() => {
    try{
        if(S7Name.value === '' || IP.value === ''  || selectCpu.value === '' || Rack.value === '' || Slot.value === ''){
            ElMessage({
                message: "请填写完整信息!", 
                type: 'error',
            });
            return; 
        }
        const S7Config = {
        S7Name: S7Name.value,
        IP: IP.value,
        port: port.value,
        selectCpu: selectCpu.value,
        Rack: Rack.value,
        Slot: Slot.value
        }
        //调用接口，将S7Config保存到数据库
        const response = await axios.post('http://127.0.0.1:5264/api/S7Config/AddS7Confug', S7Config,{
                        headers: { 'Content-Type': 'application/json' },
                    });
            if(response.data.code === 200){
                ElMessage({
                    message: "添加成功!", 
                    type: 'success',
                });
            }else{
                ElMessage({
                    message: "添加失败!错误信息为:"+response.data.msg, 
                    type: 'error',
                });
            }
        
    } catch(err) {
        ElMessage({
            message: "添加失败!错误信息为:"+err, 
            type: 'error',
        });    
    }
}


//取消
const cancel = () => {
    S7Name.value = '';
    IP.value = '';
    port.value = 102;
    selectCpu.value = '';
    Rack.value = '';
    Slot.value = '';
    ElMessage({
                message: "已清空!", 
                type: 'success',
            });
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
</style>