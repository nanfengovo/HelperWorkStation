<template>
    <div class="S7Card">
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
                        <el-form-item label="CPU型号">
                            <el-select v-model="form.CPUType" placeholder="please select your zone">
                                <el-option label="S7-1200" value="S7-1200" />
                                <el-option label="S7-1500" value="S7-1500" />
                            </el-select>
                        </el-form-item>
                    </div>
        </el-card>
        <el-card style="width: 100%" shadow="always">Hover</el-card>
    </div>
</template>
<script setup lang = "ts">
import { ref } from 'vue';
const cpuType = ref(['S7-1200', 'S7-1500', 'S7-300', 'S7-400']);
</script>

<style scoped>
.S7Card {
    width: 100%;
}
.card-top {
    width: 100%;
    height: 180px;
    margin-top: 20px;
    margin-bottom: 40px;
    background-color: #fbfbfb;
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
</style>