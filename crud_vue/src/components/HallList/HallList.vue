<template>
  <div :class="$style.root">
    <Table
        :headers="[
          {value: 'id', text: 'ID'},
          {value: 'name', text: 'Название'},
          {value: 'control', text: 'Действие'},
        ]"
        :items="items"
    >
      <template v-slot:control="{ item }">
        <Btn @click="onClickEdit(item.id)" theme="info">Изменить</Btn>
        <Btn @click="onClickRemove(item.id)" theme="danger">Удалить</Btn>
        <Btn @click="onClickShow(item.id)" theme="info">Показать записи</Btn>
      </template>
    </Table>
    <RouterLink :to="{ name: 'HallEdit' }">
      <Btn :class="$style.create" theme="info">Создать</Btn>
    </RouterLink>
  </div>
</template>

<script>
import { useStore } from 'vuex';
import { computed,    onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';

import { selectItems, removeItem, fetchItems  } from '@/store/halls/selectors'
import {} from '@/store/paintings/'
import Table from '@/components/Table/Table';
import Btn from '@/components/Btn/Btn';
export default {
  name: 'HallList',
  components: {
    Btn,
    Table,
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    onBeforeMount(() =>{
      fetchItems(store);
    });
    return {
      items: computed(() => selectItems(store)),
      onClickRemove: async (id) => {
        const isConfirmRemove = confirm('Вы действительно хотите удалить запись?')
        if (isConfirmRemove) {
          await removeItem(store, id);
          await fetchItems(store);
        }
      },
      onClickEdit: ( id ) => {
        router.push({ name: 'HallEdit', params: { id } })
      },
      onClickShow: (id) => {
        router.push({ name: 'HallPaintings', query: { hallId: id } })
}
    }
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
