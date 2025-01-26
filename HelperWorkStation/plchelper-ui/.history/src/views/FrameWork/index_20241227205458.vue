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
                            <el-menu-item index="/frameWork/PLCWorkstation/PLC">PLC配置</el-menu-item>
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
                            <!-- <div class="main-header-breadcrumb"> -->
                                <!-- 面包屑 -->
                                <!-- <el-breadcrumb :separator-icon="ArrowRight"
                                                style="cursor: pointer; margin: 10px 0; display:inline-block;margin-left: 10px; ">
                                                <el-breadcrumb-item  >面包屑导航</el-breadcrumb-item>
                                </el-breadcrumb> -->
                            <!-- </div> -->
                            <div class="main-header-tab">
                                <el-tab>
                                    <el-tabs
                                    v-model="tablist"
                                    type="card"
                                    editable
                                    class="demo-tabs"
                                    @edit="handleTabsEdit"
                                >
                                    <el-tab-pane
                                    v-for="item in tablist"
                                    :key="item.name"
                                    :label="item.name"
                                    :name="item.name"
                                    >
                                    {{ item.path }}
                                    </el-tab-pane>
                                </el-tabs>
                                </el-tab>
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
import type { TabPaneName } from 'element-plus'



const SoftVersion = ref('1.0.0')
const SoftAuthor = ref('Nanfengovo')


import { useRoute } from 'vue-router'
const route = useRoute()
const pathList = ref()

console.log('route', route)// 获取当前路由
console.log('route.matched', route.matched)// 获取当前路由匹配到的路由记录
console.log('route.matched[0].children', route.matched[0].children)// 获取当前路由匹配到的路由记录

const tablist = ref(route.matched[0].children)
const isCollapse = ref(false)

let tabIndex = 1;



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

.main-header-tab
{
    border: blue 1px solid;
    width: 100%;
    height: 30px;
}
.demo-tabs {
    border: 1px solid yellow;
    border-radius: 4px;
    margin: 0;
}
.demo-tabs>.el-tab-pane {
    border-bottom: 1px solid #ebeef5;
}
</style>