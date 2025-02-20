<template>
    <div class="DBPointCard">
        <el-card class="DBPoint-config-card" shadow="hover">
            <div class="DBPoint-header">
                <div class="card-top-top">
                        <h4>DB块配置</h4>
                </div>
                <el-form :model="DBpointConfig" class="DBPointConfig-form">
                    <div class="content">
                        <el-form-item label = "S7配置名称:">
                            <el-select 
                            v-model="selectedS7Config" 
                            placeholder="请选择S7配置" 
                            style="width: 240px"
                            :loading="isLoading"
                            clearable
                            >
                            <!-- 加载中显示提示 -->
                            <el-option v-if="isLoading" disabled label="加载中..." value="" />

                            <!-- 数据加载完成后的选项 -->
                            <el-option
                                v-for="item in selectedS7ConfigList"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "数据点名称:">
                            <el-input v-model="dbName" placeholder="数据点名称"  clearable/>
                        </el-form-item>
                        <el-form-item label = "数据类型:">
                            <el-input v-model="dbType" placeholder="请输入数据类型"  clearable/>
                        </el-form-item>
                        <el-form-item label = "DB块地址:">
                            <el-input  v-model="dbAddress" placeholder="请输入DB块地址"  clearable/>
                        </el-form-item>
                        <el-form-item label = "偏移量:">
                            <el-input v-model="dbOffset" placeholder="请输入偏移量" clearable />
                        </el-form-item>
                        <el-form-item label = "备注:">
                            <el-input v-model="remark" placeholder="请输入备注"  clearable />
                        </el-form-item>
                        <el-form-item label = "是否启用:">
                            <el-switch
                            v-model="isOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                        />
                        </el-form-item>
                    </div>
                    <div class="btn-group">
                        <el-button @click="cancel">取消此配置</el-button>
                        <el-button type="primary" @click = "AddDBPointConfig">添加此配置</el-button>
                    </div>
                </el-form>
            </div>
        </el-card>


        <el-card style="width: 100%" shadow="always" class="DBPointconfigData-card"> 
            <el-table :data="DBPointConfigData" stripe style="width: 100%" >
                <el-table-column prop="id" label="序号" width="60" />
                <el-table-column prop="dbName" label="数据点名称" width="150" />
                <el-table-column prop="s7Name" label="S7配置名称" width="150" />
                <el-table-column prop="dbType" label="数据类型" width="100" />
                <el-table-column prop="dbAddress" label="DB块地址" width="100" />
                <el-table-column prop="dbOffset" label="偏移量" width="80" />
                <el-table-column prop="remark" label="备注" width="180" />

                <el-table-column prop="isOpen" label="是否启用" width="80">
                    <template #default="scope">
                        <el-switch
                            v-model="scope.row.isOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                            disabled
                        />
                    </template>

                </el-table-column>
                
                <el-table-column label="操作" width="450">
                    <template #default="scope">
                        <el-button type="success" @click="Enable(scope.$index, scope.row)">启用</el-button>
                        <el-button type="info" @click="WatchStatus(scope.$index, scope.row)">监控</el-button>
                        <el-button type="success" @click="Write(scope.$index, scope.row)">写入</el-button>
                        <el-button type="warning" @click="Disable(scope.$index, scope.row)">停用</el-button>
                        <el-button type="primary" @click="Edit(scope.$index, scope.row)">修改</el-button>
                        <el-button type="danger" plain @click="Delete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>

        <!-- 插槽-编辑对话框 -->
        <el-dialog v-model="editFormVisible" width="500" draggable >
            <slot-header class="dialog-header">
                <H3>修改DB块配置</H3>
            </slot-header>
        <el-form :model="form">
                        <el-form-item label = "数据点名称:">
                            <el-input v-model="form.dbName" placeholder="请输入数据点名称" clearable  />
                        </el-form-item>
                        <el-form-item label = "S7配置名称:">
                            <el-select 
                            v-model="form.s7Name" 
                            placeholder="请选择S7配置" 
                            style="width: 240px"
                            :loading="isLoading"
                            Clearable
                            >
                            <!-- 加载中显示提示 -->
                            <el-option v-if="isLoading" disabled label="加载中..." value="" />

                            <!-- 数据加载完成后的选项 -->
                            <el-option
                                v-for="item in selectedS7ConfigList"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            />
                            </el-select>
                        </el-form-item>
                        <el-form-item label = "数据类型:">
                            <el-input v-model="form.dbType" placeholder="数据类型" clearable />
                        </el-form-item>
                        <el-form-item label = "DB块地址:">
                            <el-input  v-model="form.dbAddress" placeholder="请输入DB块地址"  clearable/>
                        </el-form-item>
                        <el-form-item label = "偏移量:">
                            <el-input v-model="form.dbOffset" placeholder="请输入偏移量" clearable />
                        </el-form-item>
                        <el-form-item label = "备注:">
                            <el-input v-model="form.remark" placeholder="请输入备注"  clearable />
                        </el-form-item>
                        <el-form-item label = "是否启用:">
                            <el-switch
                            v-model="form.isOpen"
                            class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
                        />
                        </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
            <el-button @click="editFormVisible = false">取消</el-button>
            <el-button type="primary" @click="update">确认</el-button>
            </div>
        </template>
        </el-dialog>
    </div>
    <!-- 抽屉:显示监控的数据信息 -->
    <el-drawer
                v-model=drawer
                :title="drawerdata?.remark"
                :data=drawerdata
                :direction="direction"
                :before-close="handleClose"
            >
            <el-table :data="tableData" style="width: 100%" >
                <el-table-column fixed prop="dbname" label="数据点名称" width="200" />
                <el-table-column prop="result" label="当前值" width="200" />
            </el-table>
    </el-drawer>

    <!-- 插槽 修改变量值 -->
    <el-dialog v-model="diaDBPoint" :title={{remarktitle}} width="500" draggable>
        <el-lable>
            当前读取值:
        </el-lable>
        
        <template #footer>
        <div class="dialog-footer">
            <el-button @click="dialogVisible = false">Cancel</el-button>
            <el-button type="primary" @click="dialogVisible = false">
            Confirm
            </el-button>
        </div>
        </template>
    </el-dialog>
</template>
<script setup lang = "ts">
import axios from 'axios';
import { ElMessage, ElMessageBox, ElNotification, type DrawerProps } from 'element-plus';
import { onMounted, onUnmounted, ref } from 'vue';


const DBpointConfig = ref({
    dbName: '',
    dbType: '',
    dbAddress: '',
    dbOffset: '',
    remark: '',
    isOpen: true
}); //DB配置数据
const selectedS7Config = ref(''); //存用户选择的S7配置名称
const isLoading = ref(true); //加载状态
const dbName = ref(''); //数据点名称
const dbType = ref(''); //数据类型
const dbAddress = ref(''); //DB块地址
const dbOffset = ref(''); //偏移量
const remark = ref(''); //备注
const isOpen = ref(true); //是否启用

const selectedS7ConfigList = ref([]); //S7配置列表
const remarktitle = ref('');
//写入变量点
const diaDBPoint = ref(false); //是否显示写入变量值对话框
const Write = (index: number,row: { id: number, s7Name: string, dbName: string, dbType: string, dbAddress: string, dbOffset: string, remark: string, isOpen: boolean}) => {
    form.value = { ...row }; //复制数据
    diaDBPoint.value = true;
    //remarktitle.value = row.remark;
}


//监控
let intervalId:any= null; // 用于存储定时器的ID
const drawer = ref(false)
const direction = ref<DrawerProps['direction']>('rtl')
    const handleClose = (done: () => void) => {
ElMessageBox.confirm('确定关闭S7变量点监控?')
    .then(() => {
        // 清空表数据
        tableData.value = [];
        // 停止定时器
        stopWatchStatus();
        // 调用done函数关闭抽屉
        done();
    })
    .catch(() => {
      // catch error
    })
}

// 当不再需要实时更新时，可以调用以下函数来清除定时器
const stopWatchStatus = () => {
    if (intervalId) {
    clearInterval(intervalId);
    intervalId = null;
    }
};
    
    // 组件卸载时停止监控
    onUnmounted(stopWatchStatus);
//编辑对话框是否显示
const editFormVisible = ref(false); 

const form = ref({
    id: 0,
    dbName: '',
    s7Name: '',
    dbType: '',
    dbAddress: '',
    dbOffset: '',
    remark: '',
    isOpen: true
}); //编辑表单数据

//s7配置名称下拉框数据
async function GetAllS7Config() {
    try {
        const res = await axios.get('http://127.0.0.1:5264/api/DBPointConfig/GetAllS7');
         // 将字符串数组转换为 { label: string, value: string } 格式
        selectedS7ConfigList.value = res.data.map((item: any) => ({
      label: item,   // 显示名称
      value: item    // 绑定值（与 label 相同）
    }));
        isLoading.value = false; // 加载完成
    } catch (error) {
        ElMessage({
            message: "获取S7列表失败,请检查后端是否启动!", 
            type: 'error',
        });
    isLoading.value = false;
    }
}

const DBPointConfigData = ref<Array<{id: number,dbName: string ,S7Name: string, DBType: string, DBAddress: string, DBOffset: string, Remark: string, IsOpen: boolean}>>([]); //DB配置数据

//获取DB配置数据
async function GetDBPointConfig() {
    try {
        const res = await axios.get('http://localhost:5264/api/DBPointConfig/GetAllDBPointConfig');
        DBPointConfigData.value = res.data;
    } catch (error) {
        ElMessage({
            message: "获取DB配置数据失败,请检查后端是否启动!", 
            type: 'error',
        });
    }
    }

//cancel按钮点击事件
function clear() {
    selectedS7Config.value = '';
    dbName.value = '';
    dbType.value = '';
    dbAddress.value = '';
    dbOffset.value = '';
    remark.value = '';
    isOpen.value = true;
}

const cancel = () => {
    ElMessageBox.confirm(
    '确定要取消保存此配置?',
    '警告',
    {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
    }
    )
    .then(() => {
        clear(); //清空数据
        ElMessage({
        type: 'success',
        message: '清除成功!',
        })
    })
    .catch(() => {
        ElMessage({
            type: 'info',
            message: '取消清除',
        })
    })
}

//AddDBPointConfig按钮点击事件
const AddDBPointConfig = () => {
    ElMessageBox.confirm(
    '确定要保存此配置?',
    '警告',
    {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
    }
    )
    .then(() => {
        //发送请求
        axios.post('http://localhost:5264/api/DBPointConfig/AddDBPointConfig', {
            S7Name: selectedS7Config.value,
            DBName: dbName.value,
            DBType: dbType.value,
            DBAddress: dbAddress.value,
            DBOffset: dbOffset.value,
            Remark: remark.value,
            IsOpen: isOpen.value
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then((res) => {
            console.log(res.data);
            if (res.data.code == 200) {
                ElMessage({
                    message: "添加成功!",
                    type: 'success',
                });
                clear(); //清空数据
                GetDBPointConfig(); //刷新数据
            } 
            else {
                ElMessage({
                    message: res.data,
                    type: 'error',
                });
            }
        })
        .catch((error) => {
            ElMessage({
                message: "添加失败,请检查数据是否正确!",
                type: 'error',
            });
        })
    })
    .catch(() => {
        ElMessage({
            type: 'info',
            message: '取消保存',
        })
    })
}



//组合挂载时加载数据
onMounted(() => {
    //加载S7配置数据
    GetAllS7Config();
    //加载DB配置数据
    GetDBPointConfig();
});

//DeleteDBPointConfig按钮点击事件
const Delete = async (index: number, row: { id: number })  => {
    ElMessageBox.confirm(
    '确定要删除此配置?',
    '警告',
    {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
    }
    )
    .then(() => {
        //发送请求
        axios.delete(`http://localhost:5264/api/DBPointConfig/DeleteDBPointConfig?id=${row.id}`)
        .then((res) => {
            console.log(res.data);
            if (res.data.code == 200) {
                ElMessage({
                    message: "删除成功!",
                    type: 'success',
                });
                GetDBPointConfig(); //刷新数据
            } 
            else {
                ElMessage({
                    message: res.data,
                    type: 'error',
                });
            }
        })
        .catch((error) => {
            ElMessage({
                message: "删除失败,请检查数据是否正确!",
                type: 'error',
            });
        })
    })
    .catch(() => {
        ElMessage({
            type: 'info',
            message: '取消删除',
        })
    })
}

//Edit
const Edit = (index: number, row: { id: number, s7Name: string, dbName: string, dbType: string, dbAddress: string, dbOffset: string, remark: string, isOpen: boolean}) => {
    form.value = { ...row }; //复制数据
    editFormVisible.value = true; //显示编辑表单
    form.value.id = row.id; //赋值
    update
}

const update = async () => {
    const res =await axios.put(`http://localhost:5264/api/DBPointConfig/UpdateDBPointConfig?Id=${form.value.id}&S7Name=${form.value.s7Name}&DBName=${form.value.dbName}&DBType=${form.value.dbType}&DBAddress=${form.value.dbAddress}&DBOffset=${form.value.dbOffset}&Remark=${form.value.remark}&isOpen=${form.value.isOpen}`,{
            headers: { 'Content-Type': 'application/json' },
        });
    if(res.data.code == 200){
        ElMessage({
            message: "修改成功!",
            type: 'success',
        });
        clearForm(); //清空数据
        GetDBPointConfig(); //刷新数据
        editFormVisible.value = false; //关闭编辑表单
    }
    else{
        ElMessage({
            message: res.data,
            type: 'error',
        });
    }
}

//清空插槽的数据
const clearForm = () => {
    form.value.dbName = '';
    form.value.s7Name = '';
    form.value.dbType = '';
    form.value.dbAddress = '';
    form.value.dbOffset = '';
    form.value.remark = '';
    form.value.isOpen = true;
}

//启用
const Enable = async (index: number, row: { id: number }) => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/DBPointConfig/EnableDBPointConfig?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            GetDBPointConfig();;
        } else if (response.data.code == 401) {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'warning',
            });
        }
        else {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'error',
            });
        }
    } catch (error: any) {
        ElMessage({
            message: error.message,
            type: 'error',
        });
    }
}

//停用
const Disable = async (index: number, row: { id: number }) => {
    try {
        const response = await axios.put(`http://127.0.0.1:5264/api/DBPointConfig/DisableDBPointConfig?id=${row.id}`);
        if (response.data.code == 200) {
            // 显示成功消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的成功消息
                type: 'success',
            });
            // 重新获取数据以更新表格
            GetDBPointConfig();;;
        } else if (response.data.code == 401) {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'warning',
            });
        }
        else {
            // 显示失败消息
            ElMessage({
                message: response.data.message, // 使用服务器返回的失败消息
                type: 'error',
            });
        }
    } catch (error: any) {
        ElMessage({
            message: error.message,
            type: 'error',
        });
    }
}

//监控
const drawerdata = ref<{ id: number, s7Name: string, dbName: string, dbType: string, dbAddress: string, dbOffset: string, remark: string, isOpen: boolean }>();
const tableData = ref<{dbname:string ,result:string}[]>([]);
const WatchStatus = async (index: number, row: { id: number, s7Name: string, dbName: string, dbType: string, dbAddress: string, dbOffset: string, remark: string, isOpen: boolean }) => {
    drawer.value = true;
    drawerdata.value = {...row};
      // 清除之前的定时器
        if (intervalId) {
    clearInterval(intervalId);
}
// 设置新的定时器，每隔1秒调用一次
intervalId = setInterval(async () => {
    try {
    const response = await axios.get(`http://127.0.0.1:5264/api/S7ReadAndWrite/ReadDBPoint?id=${row.id}`);
    if (response.data.code == 200) {
        const dataFromServer = response.data.data; // 获取后端返回的数据数组
        tableData.value.push({ dbname: row.dbName, result: dataFromServer });
        if(tableData.value.length > 5)
        {
            tableData.value = [];
        }
        ElMessage({
        message: "监控S7数据点成功!!",
        type: 'success',
        });
    } else {
        ElMessage({
        message: response.data.message,
        type: 'error',
        });
    }
    } catch (error: any) {
    ElMessage({
        message: error.message,
        type: 'error',
    });
    }
  }, 1000); // 设置间隔为1秒
};


</script>
<style>
.DBPoint-config-card{
    width: 100%;
    margin-bottom: 20px;
    background-color: #F0FFF0;
    border-radius: 20px;
    
}
.DBPoint-config-card .el-form-item{
    margin-left: 20px;
}

.DBPoint-config-card .el-input{
    width: 240px;
}

</style>