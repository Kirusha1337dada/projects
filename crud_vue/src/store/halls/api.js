import Api from '@/api/index';

class Halls extends Api {

  /**
   * Вернет список всех групп
   * @returns {Promise<Response>}
   */
  halls = () => this.rest('/halls/list.json');

  /**
   * Удалит группу по id
   * @param id
   * @returns {Promise<*>}
   */
  remove = (id) => {
    const formData = new FormData();
    formData.append('id', id);
  
    return this.rest('/halls/delete-item', {
      method: 'POST',
      body: formData
    }).then(response => response.json());
  };

  /**
   * Создаст новую запись в таблице
   * @param hall объект группы, взятый из FormGroup
   * @returns {Promise<Response>}
   */
  add = (hall) => {
    const formData = new FormData();
    formData.append('name', hall.name);

  
    return this.rest('/halls/add-item', {
      method: 'POST',
      body: formData
    }).then(response => response.json());
  }; // then - заглушка, пока метод ничего не возвращает

  /**
   * Отправит измененную запись
   * @param hall объект группы, взятый из FormGroup
   * @returns {Promise<*>}
   */
  update = (hall) => {
    const formData = new FormData();
    formData.append('id', hall.id);
    formData.append('name', hall.name);
  
    return this.rest('/halls/update-item', {
      method: 'POST',
      body: formData
    }).then(response => response.json());
  };

}

export default new Halls();
