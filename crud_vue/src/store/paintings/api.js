import Api from '@/api/index';

class Paintings extends Api {

  /**
   * Вернет список всех студентов
   * @returns {Promise<Response>}
   */
  paintings = () => this.rest('/paintings/list.json');


  /**
   * Удалит студента по id
   * @param id
   * @returns {Promise<*>}
   */
  remove = (id) => {
    const formData = new FormData();
    formData.append('id', id);
  
    return this.rest('/paintings/delete-item', {
      method: 'POST',
      body: formData
    }).then(response => response.json());
  };

  paintingId = (hallId) => this.rest(`/paintings/SelectByID?id_hall=${hallId}`);


  
   
  
  add = (painting) => {
    const formData = new FormData();
    formData.append('name', painting.name);
    formData.append('img', painting.img); 
    formData.append('year', painting.year);
    formData.append('description', painting.description);
    formData.append('id_hall', painting.id_hall);

  
    return this.rest('/paintings/add-item', {
      method: 'POST',
      body: formData
    }).then(response => response.json());
  }; 

   update = (painting) => {
    const formData = new FormData();
    formData.append('id', painting.id);
    formData.append('name', painting.name);
    formData.append('description', painting.description);
    formData.append('year', painting.year);
    formData.append('id_hall', painting.id_hall);
    formData.append('img', painting.img);
  
    return this.rest('/paintings/update-item', {
      method: 'POST',
      body: formData
    }).then(response => response.json());
  };

}

export default new Paintings();
