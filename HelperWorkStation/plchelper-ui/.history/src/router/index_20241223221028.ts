import { createMemoryHistory, createRouter, createWebHashHistory } from 'vue-router'
import frameWork from '../views/FrameWork/index.vue';
import Login from '../views/Login/index.vue';
import panel from '../views/FrameWork/layout/panel/index.vue';

const routes = [
    { path: '/', component: Login },
    { path: '/framework', component: frameWork },
    { path: '/framework/panel', component: frameWork ,childrenn:[{
                    path: '/panel',
        
                    name: '仪表盘',
        
                    component: () => import('../views/FrameWork/layout/panel/index.vue'),
        
                    meta: { title: '仪表盘', requireAuth: true, affix: true, closable: false },}]},
    ]


    const router = createRouter({
        history: createWebHashHistory(),
        routes,
    })

export default router;