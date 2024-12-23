<template>
    <div class="common-layout">
        <el-container>
            <div class="layout-header">
                <el-header>
                    PLC通讯
                </el-header>
            </div>
            <el-container>
                <el-aside width="200px">
                    <el-radio-group v-model="isCollapse" style="margin-bottom: 20px">
                        <el-radio-button :value="false"><el-icon><Expand /></el-icon></el-radio-button>
                        <el-radio-button :value="true"><el-icon><Fold /></el-icon></el-radio-button>
                    </el-radio-group>
                    <el-menu
                        default-active="1"
                        class="el-menu-vertical-demo"
                        :collapse="isCollapse"
                        @open="handleOpen"
                        @close="handleClose"
                    >
                    <el-menu-item index="1">
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
                            <el-menu-item index="2-1">Modbus模块</el-menu-item>
                            <el-menu-item index="2-2">S7模块</el-menu-item>
                            <el-menu-item index="2-3">OPC模块</el-menu-item>
                            <el-menu-item index="2-4">FinsTCP模块</el-menu-item>
                        </el-menu-item-group>
                        </el-sub-menu>
                        <el-menu-item index="3">
                        <el-icon><setting /></el-icon>
                        <template #title>Navigator Four</template>
                        </el-menu-item>
                    </el-menu>
                    </el-aside>
                    <el-container>
                        <el-main>Main</el-main>
                    </el-container>
            </el-container>
            <el-footer>Footer</el-footer>
        </el-container>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import {
    Document,
    Menu as IconMenu,
    Location,
    Setting,
} from '@element-plus/icons-vue'

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
    background-color: #409EFF;
    color: #fff;
    text-align: center;
    line-height: 60px;
}

</style>