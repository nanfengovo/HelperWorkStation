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
                        default-active="2"
                        class="el-menu-vertical-demo"
                        :collapse="isCollapse"
                        @open="handleOpen"
                        @close="handleClose"
                    >
                    <el-menu-item index="1">
                        <el-icon><icon-menu /></el-icon>
                        <template #title>Navigator one</template>
                        </el-menu-item>
                        <el-sub-menu index="2">
                        <template #title>
                            <el-icon><location /></el-icon>
                            <span>Navigator two</span>
                        </template>
                        <el-menu-item-group>
                            <template #title><span>Group One</span></template>
                            <el-menu-item index="2-1">item one</el-menu-item>
                            <el-menu-item index="2-2">item two</el-menu-item>
                        </el-menu-item-group>
                        <el-menu-item-group title="Group Two">
                            <el-menu-item index="2-3">item three</el-menu-item>
                        </el-menu-item-group>
                        <el-sub-menu index="2-4">
                            <template #title><span>item four</span></template>
                            <el-menu-item index="2-4-1">item one</el-menu-item>
                        </el-sub-menu>
                        </el-sub-menu>
                       
                        <el-menu-item index="3" disabled>
                        <el-icon><document /></el-icon>
                        <template #title>Navigator Three</template>
                        </el-menu-item>
                        <el-menu-item index="4">
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