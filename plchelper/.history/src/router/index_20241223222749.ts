import { createMemoryHistory, createRouter, createWebHashHistory } from 'vue-router'
import frameWork from '../views/FrameWork/index.vue';
import Login from '../views/Login/index.vue';
import panel from '../views/FrameWork/layout/panel/index.vue';

const routes = [
    {
        path: '/',
        name: 'login',
        component: Login,
    },
    {
        path: '/frameWork',
        name: 'frameWork',
        component: frameWork,
        children: [
            {
                path: '/frameWork/panel',
                name: 'panel',
                component: panel,
            },
        ],
        
    }
    ]


    const router = createRouter({
        history: createWebHashHistory(),
        routes,
    })

export default router;