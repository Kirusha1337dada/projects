<template>
  <div :class="$style.root">
    <Table
      :headers="[
        {value: 'id', text: 'ID'},
        {value: 'name', text: 'Имя'},
        {value: 'img', text: 'Изображение'},
        {value: 'year', text: 'Год'},
        {value: 'description', text: 'Описание'},
        {value: 'hall_name', text: 'Зал'},  
        {value: 'control', text: 'Действие'},
      ]"
      :items="items"
    >
      <template v-slot:control="{ item }">
        <Btn @click="onClickEdit(item.id)" theme="info">Изменить</Btn>
        <Btn @click="onClickRemove(item.id)" theme="danger">Удалить</Btn>
      </template>
      <template v-slot:img="{ item }">
        <img :src="getImgUrl(item.img)" alt="Изображение" width="100px" height="100px">
      </template>
    </Table>
    <router-link :to="{ name: 'PaintingEdit' }">
      <Btn :class="$style.create" theme="info">Создать</Btn>
    </router-link>
  </div>
</template>

<script>
import { useStore } from 'vuex';
import { computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';

import { selectItems, removeItem, fetchItems } from '@/store/paintings/selectors';
import { selectItemById, fetchItems as fetchHalls } from '@/store/halls/selectors';
import Table from '@/components/Table/Table';
import Btn from '@/components/Btn/Btn';

export default {
  name: 'PaintingList',
  components: {
    Table,
    Btn,
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    onMounted(() => {
      fetchItems(store);
      fetchHalls(store);
    });
    return {
      items: computed(() => {
        return selectItems(store).map(item => ({
          ...item,
          id_hall: selectItemById(store, item.id_hall).name
        }));
      }),
      onClickRemove: async id => {
        const isConfirmRemove = confirm('Вы действительно хотите удалить запись?');
        if (isConfirmRemove) {
          try {
            removeItem(store, id);
            await new Promise(resolve => setTimeout(resolve, 50)); 
            fetchItems(store);
            fetchHalls(store);
          } catch (error) {
            console.error('Ошибка при удалении записи и загрузке данных:', error);
          }
        }
      },   

      onClickEdit: id => {
        router.push({ name: 'PaintingEdit', params: { id } })
      },
      getImgUrl: imgPath => {
        return require( '@/assets/' + imgPath);
      }
    }
  }

}
</script>

<style module lang="scss">
.root {

  .create {
    margin-top: 16px;
  }

}
</style>
