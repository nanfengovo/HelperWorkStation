<template>
    <div class="S7Card">
        <el-card class="card-top" shadow="hover">
                    <div class="card-top-top">
                        <h4>S7配置</h4>
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
                        <div class=content-cputype>
                            <label>CPU型号:</label>
                            <el-select  v-model="selectCpu" placeholder="请选择CPU型号" style="width: 240px">
                                <el-option label="S7-1200" value="S7-1200" />
                                <el-option label="S7-1500" value="S7-1500" />
                            </el-select>
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
                        <div class="content-rack"> 
                            <label>Rack:</label>
                            <el-input
                                v-model="inputRack"
                                style="width: 240px"
                                placeholder="请输入PLC的Rack"
                                clearable
                            />
                        </div>
                        <div class="content-slot"> 
                            <label>slot:</label>
                            <el-input
                                v-model="inputRack"
                                style="width: 240px"
                                placeholder="请输入PLC的slot"
                                clearable
                            />
                        </div>
                        <div class="btn-save">
                            <el-button type="primary">保存</el-button>
                        </div>
                        <div class="btn-cancel">
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

</script>

<style scoped>
.S7Card {
    width: 100%;
}
.card-top {
    width: 100%;
    height: 200px;
    margin-top: 20px;
    margin-bottom: 40px;
    background-color: #F0FFF0;
    border-radius: 12px; 
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