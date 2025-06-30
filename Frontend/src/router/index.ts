import { createRouter , createWebHistory } from 'vue-router'
import FirstPage from '../pages/FirstPage.vue';
import DiaryPage from '../pages/DiaryPage.vue';
import TodoPage from '../pages/TodoPage.vue';

const routes = [
    { path: '/', component: FirstPage },
    { path: '/diary', component: DiaryPage},
    { path: '/todo', component: TodoPage}
]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;