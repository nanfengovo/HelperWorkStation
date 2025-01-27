<template>
    <div class="PLCCard">
        <el-card class="card-top" shadow="hover">
                    <div class="card-top-top">
                        <h4>PLC配置</h4>
                    </div>
                    <div class="card-top-content">
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
                        <div class="btn-save" v-on:click="save">
                            <el-button type="primary" >保存</el-button>
                        </div>
                        <div class="btn-cancel" v-on:click="cancel">
                            <el-button >取消</el-button>
                        </div>
                    </div>
        </el-card>
        <el-card style="width: 100%" shadow="always">显示已经配置好的保存到数据库的数据</el-card>
    </div>
</template>
<script setup lang = "ts">
import { ElMessage } from 'element-plus';
import { ref } from 'vue';
import { savePLCConfig } from '@/api/PLCConfig';
//pLc配置

interface PLC {
    inputIP: string;
    inputPort: number;
}

const inputIP = ref('');
const inputPort = ref('');


//取消
const cancel = () => {
    inputIP.value = '';
    inputPort.value = '';
}


//保存
const save = () => {
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
    const saveConfig: PLC = {
        inputIP: inputIP.value,
        inputPort: parseInt(inputPort.value),
    };
    savePLCConfig(saveConfig).then((response) => {
        ElMessage({
        message: '保存成功',
        type: 'success',
    })
    }).catch((error) => {
        ElMessage({
        message: '保存失败',
        type: 'error',
    })
    })
    inputIP.value = '';
    inputPort.value = '';




    
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