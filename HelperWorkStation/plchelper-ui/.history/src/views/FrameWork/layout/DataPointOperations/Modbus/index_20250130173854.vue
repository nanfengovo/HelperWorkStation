<template>
    <div class="modbus">
        <el-card class="modbus-card" shadow="hover">
            <div class="modbus-header">
                <div class="card-top-top">
                        <h4>Modbus配置</h4>
                </div>
                <el-form :model="modbusConfig" class="modbusConfig-form">
                    <div class="content">
                        <el-form-item label = "选择PLC:">
                            <el-select  placeholder="请选择PLC" style="width: 240px">
                                <el-option
                                    v-for="item in PLCList"
                                    
                                ></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "设备ID:">
                            <el-input-number v-model="num" :min="1" :max="32" @change="handleChange" />
                        </el-form-item>
                        <el-form-item label="Function code" class="function-code" >
                                <el-select
                                    v-model="functionCodes"
                                    style="width: 240px;"
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
                        <el-form-item label="起始地址:" >
                            <el-input v-model="address" placeholder="请输入地址" />
                        </el-form-item>
                        <el-form-item label="数量:" >
                            <el-input v-model="quantity" placeholder="请输入数量" />
                        </el-form-item>
                    </div>
                    
                    <div class="btn-group">
                        <el-button >取消此配置</el-button>
                        <el-button type="primary">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>
    </div>
</template>
<script setup lang = "ts">
import axios from 'axios';
import { onMounted, reactive, ref } from 'vue';

// const modbusConfig = reactive({
//     functionCodes: '',
//     address: ''
// });


const num = ref(1);

const functionCodes = ref('');
const address = ref('');
const quantity = ref('');
const PLCList = ref([]);

const handleChange = (value: number) => {
    console.log('Value changed:', value);
};

const GetAllPLCConfig = async () => {
    const response = await axios.get("http://127.0.0.1:5264/api/ModbusConfig/GetPLCConfig");
    console.log(response.data);
    PLCList.value = response.data;
    console.log(PLCList);
}

onMounted(() => {
    GetAllPLCConfig();
});
</script>

<style >
.content{
    display: flex;
    margin: 20px;
}
.modbusConfig-form .el-form-item{
    margin-right: 20px;
}
.btn-group{
    display: flex;
    justify-content: right;
    margin-right: 40px;
    margin-top: 50px;
}
.function-code>.el-select{
    width: 240px;
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