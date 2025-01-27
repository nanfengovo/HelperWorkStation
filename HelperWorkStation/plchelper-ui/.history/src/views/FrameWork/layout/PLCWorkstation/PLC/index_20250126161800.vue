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
                        <div class="btn-save">
                            <el-button type="primary">保存</el-button>
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
import { ref } from 'vue';

//pLc配置
const inputIP = ref('');
const selectCpu = ref('');
const inputPort = ref('');
const inputRack = ref('');


const cancel = () => {
    inputIP.value = '';
    inputPort.value = '';
    inputRack.value = '';
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