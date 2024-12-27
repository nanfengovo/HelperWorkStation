import { createMemoryHistory, createRouter, createWebHashHistory, type RouteRecordRaw } from 'vue-router'
import frameWork from '../views/FrameWork/index.vue';
import Login from '../views/Login/index.vue';
import panel from '../views/FrameWork/layout/panel/index.vue';

// const routes: RouteRecordRaw[] = [];
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
                path: '/frameWork/PLCWorkstation/PLC配置',
                name: 'PLC工作站-PLC配置',
                component: () => import('../views/FrameWork/layout/PLCWorkstation/PLC/index.vue'),
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

const router = createRouter({
    history: createWebHashHistory(),
    routes,
});

export default router;