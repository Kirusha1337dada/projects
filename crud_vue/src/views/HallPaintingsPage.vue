<template>
    <div :class="$style.root">
      <Table
        :headers="[
          { value: 'id', text: 'ID' },
          { value: 'name', text: 'Имя' },
          { value: 'img', text: 'Изображение' },
          { value: 'year', text: 'Год' },
          { value: 'description', text: 'Описание' },
          { value: 'hall_name', text: 'Зал' },
          { value: 'control', text: 'Действие' },
        ]"
        :items="items"
      >
        <template v-slot:img="{ item }">
          <img :src="getImgUrl(item.img)" alt="Изображение" style="width: 100px; height: auto;">
        </template>
        <template v-slot:control="{ item }">
          <Btn @click="onClickEdit(item.id)" theme="info">Изменить</Btn>
          <Btn @click="onClickRemove(item.id)" theme="danger">Удалить</Btn>
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
  
  import { removeItem, fetchItemsByHallId } from '@/store/paintings/selectors';
  import  { } from '@/store/halls/selectors';
  import Table from '@/components/Table/Table';
  import Btn from '@/components/Btn/Btn';
  
  export default {
    name: 'HallPaintingsPage',
    components: {
      Table,
      Btn,
    },
    setup() {
      const store = useStore();
      const router = useRouter();
  
      onMounted(() => {
      const hallId = router.currentRoute.value.query.hallId;
      fetchItemsByHallId(store, hallId);
    });

    const items = computed(() => store.state.paintings.itemsByHallId);
  
  
      const onClickRemove = id => {
        const isConfirmRemove = confirm('Вы действительно хотите удалить запись?')
        if (isConfirmRemove) {
          removeItem(store, id)
        }
      };
  
      const onClickEdit = id => {
        router.push({ name: 'PaintingEdit', params: { id } })
      };
      const getImgUrl = imgPath => {
      return require('@/assets/' + imgPath);
      };
      
      console.log(items);
  
      return {
        items,
        onClickRemove,
        onClickEdit,
        getImgUrl
        
      };
    },
  }
  </script>
  
  <style module lang="scss">
  .root {
    .create {
      margin-top: 16px;
    }
  }
  </style>
  