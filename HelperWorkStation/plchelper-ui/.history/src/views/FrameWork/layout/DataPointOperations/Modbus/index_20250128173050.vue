<template>
    <div class="modbus">
        <el-card class="modbus-card" shadow="hover">
            <div class="modbus-header">
                <div class="card-top-top">
                        <h4>Modbus配置</h4>
                </div>
                <div class="card-top-content">
                        <div class="content-plc">
                            <label>选择plc:</label>
                            <el-dropdown>
                                <el-button type="primary">
                                    PLC配置<el-icon class="el-icon--right"><arrow-down /></el-icon>
                                </el-button>
                                <template #dropdown>
                                    <el-dropdown-menu v-model="PLCList">
                                    
                                    </el-dropdown-menu>
                                </template>
                            </el-dropdown>
                        </div>
                        <div class="content-saliveId">
                            <label>设备ID:</label>
                            <el-input
                                v-model="inputSaliveId"
                                style="width: 240px"
                                placeholder="请输入设备id"
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
import { ref } from 'vue';

const inputPort = ref('');
const inputSaliveId = ref('');
const PLCList = ref([]);

</script>