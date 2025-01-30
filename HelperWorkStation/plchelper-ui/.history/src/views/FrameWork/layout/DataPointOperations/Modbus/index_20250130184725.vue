<template>
    <div class="modbus">
        <el-card class="modbusconfig-card" shadow="hover">
            <div class="modbus-header">
                <div class="card-top-top">
                        <h4>Modbus配置</h4>
                </div>
                <el-form :model="modbusConfig" class="modbusConfig-form">
                    <div class="content">
                        <el-form-item label = "选择PLC:">
                            <el-select 
                            v-model="selectedPLC" 
                            placeholder="请选择PLC" 
                            style="width: 240px"
                            :loading="isLoading"
                            >
                            <!-- 加载中显示提示 -->
                            <el-option v-if="isLoading" disabled label="加载中..." value="" />

                            <!-- 数据加载完成后的选项 -->
                            <el-option
                                v-for="item in PLCList"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "设备ID:">
                            <el-input-number v-model="slaveId" :min="1"   />
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
                        <el-form-item label = "数量:">
                            <el-input-number v-model="num" :min="1" :max="32"  />
                        </el-form-item>
                        <el-form-item label = "配置名称:">
                            <el-input v-model="ModbusName" placeholder="请输入保存的配置名称" />
                        </el-form-item>
                    </div>
                    
                    <div class="btn-group">
                        <el-button @click="cancel">取消此配置</el-button>
                        <el-button type="primary">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>


        <el-card style="width: 100%" shadow="always" class="Modbus-card" >
            <el-table :data="ModbusData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="180" />
                <el-table-column prop="MobusName" label="Modbus配置的名称" width="180" />
                <el-table-column prop="name" label="PLC名称" width="180" />
                <el-table-column prop="salveId" label="设备ID" />
                <el-table-column prop="functionCodes" label="功能码" />
                <el-table-column prop="address" label="起始地址" />
                <el-table-column prop="quantity" label="数量" />
                <el-table-column label="操作" width="300">
                    <template #default="scope">
                        <el-button type="primary" @click="Edit(scope.$index, scope.row)">修改</el-button>
                        <el-button type="danger" plain @click="Delete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
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
const slaveId = ref(1);
const functionCodes = ref('');
const address = ref('');
const quantity = ref('');
const ModbusName = ref('');
const selectedPLC = ref(''); // 用于存储用户选择的值
const PLCList = ref([]);       // 存储格式化后的选项
const isLoading = ref(true);   // 加载状态



const GetAllPLCConfig = async () => {
    try{
        const response = await axios.get("http://127.0.0.1:5264/api/ModbusConfig/GetPLCConfig");
    
    // 将字符串数组转换为 { label: string, value: string } 格式
    PLCList.value = response.data.map((item: any) => ({
      label: item,   // 显示名称
      value: item    // 绑定值（与 label 相同）
    }));

    isLoading.value = false;
    }
    
    catch (error) {
    console.error('获取PLC列表失败:', error);
    isLoading.value = false;
    }
    // console.log(response.data);
    // PLCList = response.data;
    // console.log(PLCList);
}
//加载下拉框数据
onMounted(() => {
    GetAllPLCConfig();
});

//取消配置
const cancel = () => {
    functionCodes.value = '';
    slaveId.value = 1;
    address.value = '';
    quantity.value = '';
    selectedPLC.value = '';
    num.value = 1;
}
</script>

<style >
.content{
   
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
.modbusconfig-card{
    width: 100%;
    height: 300px;
    background-color: #F0FFF0;
    border-radius: 20px;
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
}
.Modbus-card{
    margin-top: 40px;
    border-radius: 15px;
}

</style>