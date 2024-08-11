<template>
  <Layout :title="id ? 'Редактирование записи' : 'Создание записи'">
    <HallForm
        :id="id"
        @submit="onSubmit"
    />
  </Layout>
</template>

<script>
import { useStore } from 'vuex';

import { updateItem, addItem } from '@/store/halls/selectors';
import Layout from '@/components/Layout/Layout';
import HallForm from '@/components/HallForm/HallForm';
export default {
  name: 'HallEdit',
  props: {
    id: String,
  },
  components: {
    Layout,
    HallForm,
  },
  setup() {
    const store = useStore();
    return {
      onSubmit: ({ id, name }) => id ?
          updateItem(store, { id, name }) :
          addItem(store, { name }),
    };
  }
}
</script>

