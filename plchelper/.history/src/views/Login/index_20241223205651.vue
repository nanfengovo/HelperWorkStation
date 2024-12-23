<template>
    <div class="login-container">
        <div class="Login">
        <span>
            <h2>欢迎登录</h2>
        </span>
        <div class="Login-item">
            <el-input
            v-model="loginForm.UserName"
            style="width: 80%; height: 40px"
            clearable
            placeholder="请输入用户名"
            :prefix-icon="User"
            />
        </div>
        <div class="Login-item">
            <el-input
            v-model="loginForm.Password"
            style="width: 80%; height: 40px"
            clearable
            placeholder="请输入密码"
            type="password"
            show-password
            :prefix-icon="Lock"
            />
        </div>
        <div class="Login-item">
            <el-button
            @click="loginClick"
            type="primary"
            style="width: 80%; height: 50px"
            >登录
            </el-button>
        </div>
        </div>
    </div>
</template>


<script setup lang="ts">
import { reactive } from 'vue';

const loginForm = reactive({
    UserName: 'admin',
    Password: '123456',
})

</script>