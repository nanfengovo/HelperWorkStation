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
                            <el-select v-model="PLCList" placeholder="请选择PLC" style="width: 240px">
                            </el-select>
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
                        <!-- <el-form-item label="Function code" >
                            <el-select
                                v-model="formInline.region"
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
                        </el-form-item> -->
                        <el-form :inline="true" :model="formInline" class="demo-form-inline">
                            <el-form-item label="Activity zone" >
                            <el-select
                                v-model="formInline.region"
                                
                                placeholder="Activity zone"
                                clearable
                            >
                                <el-option label="Zone one" value="shanghai" />
                                <el-option label="Zone two" value="beijing" />
                            </el-select>
                        </el-form-item>
                        </el-form>
                        
                        <div class="content-address">
                            <label>地址:</label>
                            <el-input
                                v-model="inputAddress"
                                style="width: 240px"
                                placeholder="请输入地址"
                                clearable
                            />
                        </div>
                        <div class="content-quantity">
                            <label>数量:</label>
                            <el-input
                                v-model="inputQuantity"
                                style="width: 240px"
                                placeholder="请输入数量"
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
import { reactive, ref } from 'vue';

const inputPort = ref('');
const inputSaliveId = ref('');
const PLCList = ref([]);



const formInline = reactive({
  user: '',
  region: '',
  date: '',
})
</script>

<style >
.card-top-top{
    justify-content: center; /* 在主轴上居中对齐 */
}
.card-top-top h4{
    font-size: 20px;
    font-weight: bold;
    text-align: center;
}
.demo-form-inline .el-select {
    --el-select-width: 220px;
}</style>