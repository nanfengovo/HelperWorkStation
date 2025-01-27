<template>
    <div class="modbus">
        <el-card class="modbus-card" shadow="hover">
            <div class="modbus-header">
                <div class="card-top-top">
                        <h4>Modbus配置</h4>
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
            </div>
           
        </el-card>
    </div>
</template>
<script setup lang = "ts">
</script>