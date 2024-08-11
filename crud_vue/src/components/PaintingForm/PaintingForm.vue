<template>
  <div :class="$style.root">
    <div v-if="form.id" :class="$style.item">
      <div :class="$style.label">
        <label for="id">ID</label>
      </div>
      <input v-model="form.id" disabled :class="$style.input"  id="id" placeholder="id" type="text">
    </div>
    <div :class="$style.item">
      <div :class="$style.label">
        <label for="name">Название</label>
      </div>
      <input v-model="form.name" :class="$style.input"  id="name" placeholder="Имя" type="text">
    </div>
    <div :class="$style.item">
      <div :class="$style.label">
        <label for="img">Изображение</label>
      </div>
      <input @change="onFileChange" :class="$style.input" id="img" type="file" name="img">
    </div>
    <div :class="$style.item">
      <div :class="$style.label">
        <label for="year">Год</label>
      </div>
      <input v-model="form.year" :class="$style.input" id="year" placeholder="Год" type="text">
    </div>
    <div :class="$style.item">
      <div :class="$style.label">
        <label for="description">Описание</label>
      </div>
      <input v-model="form.description" :class="$style.input" id="description" placeholder="Описание" type="text">
    </div>
    <div :class="$style.item">
      <div :class="$style.label">
        <label for="id_hall">Зал</label>
      </div>
      <select v-model="form.id_hall" :class="$style.select" name="id_hall" id="id_hall">
        <option v-for="({ name, id  }) in hallList" :key="id" :value="id">
          {{ name }}
        </option>
      </select>
    </div>
    <div :class="$style.item">
      <Btn @click="onClick" :disabled="!isValidForm" theme="info">Сохранить</Btn>
    </div>
  </div>
</template>

<script>
import { computed, reactive,  watchEffect, onMounted } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

import { selectItemById, fetchItems } from '@/store/paintings/selectors';
import { selectItems as selectHalls, fetchItems as fetchHalls } from '@/store/halls/selectors';
import Btn from '@/components/Btn/Btn';

export default {
  name: 'PaintingForm',
  props: {
    id: { type: String, default: '' },
  },
  components: {
    Btn,
  },
  setup(props, context) {
    const store = useStore();
    const router = useRouter();
    const hallList = computed(() => selectHalls(store))
    const form = reactive({
      id: '',
      name: '',
      img: null,
      year: '',
      description: '',
      id_hall: '',

    });

    onMounted(() => {
      fetchItems(store);
      fetchHalls(store);
    });

    const onFileChange = (event) => {
      const file = event.target.files[0];
      form.img = file;
    };

    watchEffect(() => {
      const painting = selectItemById( store,  props.id );
      Object.keys(painting).forEach(key => {
        form[key] = painting[key]
      })
    });

    

    return {
      hallList,
      form,
      isValidForm: computed(() =>  !!(form.name && form.img && form.year && form.description && form.id_hall)),
      onClick: () => {
        
        context.emit('submit', form);
        router.push({ name: 'Paintings' })

      },
      onFileChange
    }
  },
}
</script>

<style module lang="scss">
.root {
  padding-top: 30px;
  max-width: 900px;

  .item {
    display: flex;
    align-items: center;

    & + .item {
      margin-top: 15px;
    }
  }

  .label {
    flex: 0 0 150px
  }

  .input {
    display: block;
    width: 100%;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    line-height: 1.5;
    color: #212529;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
  }

  .select {
    display: block;
    width: 100%;
    padding: 0.375rem 2.25rem 0.375rem 0.75rem;
    -moz-padding-start: calc(0.75rem - 3px);
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #212529;
    background-color: #fff;
    background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='%23343a40' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M2 5l6 6 6-6'/%3e%3c/svg%3e");
    background-repeat: no-repeat;
    background-position: right 0.75rem center;
    background-size: 16px 12px;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    appearance: none;
  }
}
</style>
