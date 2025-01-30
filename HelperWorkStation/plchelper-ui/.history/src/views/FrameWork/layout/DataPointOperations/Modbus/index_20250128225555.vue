<template>
    <div class="modbus">
        <el-card class="modbus-card" shadow="hover">
            <div class="modbus-header">
                <div class="card-top-top">
                        <h4>Modbus配置</h4>
                </div>
                <el-form :model="modbusConfig" class="modbusConfig-form">
                    <el-form-item label = "选择PLC:">
                        <el-select v-model="PLCList" placeholder="请选择PLC" style="width: 240px"/>
                    </el-form-item>
                    <el-form-item label = "设备ID:">
                        <el-input-number v-model="num" :min="1" :max="32" @change="handleChange" />
                    </el-form-item>
                    <el-form-item label="Function code" >
                            <el-select
                                v-model="modbusConfig.functionCodes"
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
                    </el-form-item>
                    <el-form-item label="Address" >
                        <el-input v-model="modbusConfig.address" placeholder="请输入地址" />
                    </el-form-item>
                </el-form>
            </div>
        </el-card>
    </div>
</template>
<script setup lang = "ts">
import { reactive, ref } from 'vue';

const modbusConfig = reactive({
    functionCodes: '',
    address: ''
});

const PLCList = ref('');
const num = ref(1);

const handleChange = (value: number) => {
    console.log('Value changed:', value);
};

</script>

<style >
.modbusConfig-form{
    display: flex;
    margin: 20px;
}
.modbusConfig-form .el-form-item{
    margin-right: 20px;
}
.modbus-card{
    width: 100%;
    height: 300px;
    background-color: #F0FFF0;
}
.card-top-top h4{
    font-size: 20px;
    font-weight: bold;
    text-align: center;
}
.content-plc{
    margin-left: 40px;
}
.demo-form-inline .el-select {
    --el-select-width: 220px;
}</style>