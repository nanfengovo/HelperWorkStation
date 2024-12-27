<template>
    <div class="common-layout">
        <el-container>
            <div class="layout-header">
                <el-header>
                    PLC通讯
                </el-header>
            </div>
            <el-container>
                <el-aside width="200px" height="100vh" >
                    <el-radio-group v-model="isCollapse" style="margin-bottom: 20px">
                        <el-radio-button :value="false"><el-icon><Expand /></el-icon></el-radio-button>
                        <el-radio-button :value="true"><el-icon><Fold /></el-icon></el-radio-button>
                    </el-radio-group>
                    <el-menu
                        background-color="#f5f5e9"
                        default-active="1"
                        class="el-menu-vertical-demo"
                        :collapse="isCollapse"
                        @open="handleOpen"
                        @close="handleClose"
                        router
                    >
                        <el-menu-item index="/frameWork/panel">
                            <el-menu-item >
                                <el-icon><icon-menu /></el-icon>
                                <template #title>仪表盘</template>
                            </el-menu-item>
                        </el-menu-item>
                        <el-sub-menu index="2">
                        <template #title>
                            <el-icon><location /></el-icon>
                            <span>PLC工作站</span>
                        </template>
                        <el-menu-item-group >
                            <el-menu-item index="/frameWork/PLCWorkstation/Modbus">Modbus模块</el-menu-item>
                            <el-menu-item index="/frameWork/PLCWorkstation/S7">S7模块</el-menu-item>
                            <el-menu-item index="/frameWork/PLCWorkstation/OPC">OPC模块</el-menu-item>
                            <el-menu-item index="/frameWork/PLCWorkstation/FinsTCP">FinsTCP模块</el-menu-item>
                        </el-menu-item-group>
                        </el-sub-menu>
                        <el-sub-menu index="3">
                        <template #title>
                            <el-icon><Setting /></el-icon>
                            <span>数据点运维</span>
                        </template>
                        <el-menu-item-group >
                            <el-menu-item index="/frameWork/DataPointOperations/Modbus">Modbus数据点配置</el-menu-item>
                            <el-menu-item index="/frameWork/DataPointOperations/S7">S7数据点配置</el-menu-item>
                            <el-menu-item index="/frameWork/DataPointOperations/Opc">OPC数据点配置</el-menu-item>
                            <el-menu-item index="/frameWork/DataPointOperations/FinsTCP">FinsTCP数据点模块</el-menu-item>
                        </el-menu-item-group>
                        </el-sub-menu>
                        <el-sub-menu index="4">
                        <template #title>
                            <el-icon><Document /></el-icon>
                            <span>日志模块</span>
                        </template>
                        <el-menu-item-group >
                            <el-menu-item index="/frameWork/Log/Operation">操作日志</el-menu-item>
                            <el-menu-item index="/frameWork/Log/ReadWrite">读写日志</el-menu-item>
                        </el-menu-item-group>
                        </el-sub-menu>
                    </el-menu>
                </el-aside>
                    <el-container>
                        <el-main>
                            <div class="main-header">
                            <div class="main-header-breadcrumb">
                                <!-- 面包屑 -->
                                <el-breadcrumb :separator-icon="ArrowRight"
                                                style="cursor: pointer; margin: 10px 0; display:inline-block;margin-left: 10px; ">
                                                <el-breadcrumb-item :to="item.options.routes[2].children.path" v-for="item in routes" :key="item.options.routes[2].children.name">{{ item.options.routes[2].children.name }}</el-breadcrumb-item>
                                </el-breadcrumb>
                            </div>
                            <div class="main-header-tab">

                            </div>
                        </div>
                            <RouterView/>
                        </el-main>
                    </el-container>
            </el-container>
                <el-footer class="fixed-footer" >
                    <span>版本号:{{SoftVersion}}</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>by {{ SoftAuthor }}</span>
                </el-footer>
        </el-container>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router';
import routes from '../../router/index';
import {
    Document,
    Menu as IconMenu,
    Location,
    Setting,
    ArrowRight,
} from '@element-plus/icons-vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        redirect: '/login'
    },
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
        path: '/frameWork',
        name: '主页',
        component: frameWork,
        children: [
            {
                path: '/framework/panel',
                name: '仪表板',
                component: () => import('../views/FrameWork/layout/panel/index.vue'),
            },
            {
                path: '/frameWork/PLCWorkstation/Modbus',
                name: 'PLC工作站-Modbus',
                component: () => import('../views/FrameWork/layout/PLCWorkstation/Modbus/index.vue'),
            },
            {
                path: '/frameWork/PLCWorkstation/S7',
                name: 'PLC工作站-S7',
                component: () => import('../views/FrameWork/layout/PLCWorkstation/S7/index.vue'),
            },
            {
                path: '/frameWork/PLCWorkstation/OPC',
                name: 'PLC工作站-OPC',
                component: () => import('../views/FrameWork/layout/PLCWorkstation/OPC/index.vue'),
            },
            {
                path: '/frameWork/PLCWorkstation/FinsTCP',
                name: 'PLC工作站-FinsTCP',
                component: () => import('../views/FrameWork/layout/PLCWorkstation/FinsTCP/index.vue'),
            },
            {
                path: '/frameWork/DataPointOperations/Modbus',
                name: '数据点运维-Modbus',
                component: () => import('../views/FrameWork/layout/DataPointOperations/Modbus/index.vue'),
            },
            {
                path: '/frameWork/DataPointOperations/S7',
                name: '数据点运维-S7',
                component: () => import('../views/FrameWork/layout/DataPointOperations/S7/index.vue'),
            },
            {
                path: '/frameWork/DataPointOperations/Opc',
                name: '数据点运维-Opc',
                component: () => import('../views/FrameWork/layout/DataPointOperations/OPC/index.vue'),
            },
            {
                path: '/frameWork/DataPointOperations/FinsTCP',
                name: '数据点运维-FinsTCP',
                component: () => import('../views/FrameWork/layout/DataPointOperations/FinsTCP/index.vue'),
            },
            {
                path: '/frameWork/Log/Operation',
                name: '用户操作日志',
                component: () => import('../views/FrameWork/layout/Log/Operation/index.vue'),
            },
            {
                path: '/frameWork/Log/ReadWrite',
                name: '数据点读写日志',
                component: () => import('../views/FrameWork/layout/Log/ReadWrite/index.vue'),
            },

        ],


    },
];


const activeIndex = ref('1')
const activeIndex2 = ref('1')
const handleSelect = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
                }
const router = useRouter();
const SoftVersion = ref('1.0.0')
const SoftAuthor = ref('Nanfengovo')



const isCollapse = ref(false)
const handleOpen = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}
const handleClose = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}
</script>

<style>
.el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 200px;
    min-height: 400px;
}
.layout-header {
    background-color: #f5f5e9;
    color: black;
    text-align: center;
    line-height: 60px;
}
.common-layout{
    display: flex;
    flex-direction: column;
    min-height: 100vh;
    background-color: #fffff3;
}
.fixed-footer {
    background-color: #fffff3; /* 根据需要设置背景颜色 */
    padding: 10px; /* 根据需要设置内边距 */
    text-align: center; /* 根据需要设置文本对齐方式 */
    margin-bottom: 20px;
}
.main-header{
    border: red 1px solid;
    width: 100%;
    height: 30px;
}


</style>