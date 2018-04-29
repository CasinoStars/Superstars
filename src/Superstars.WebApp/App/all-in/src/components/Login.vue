<template>
  <div id="home">
    <h1>Bienvenue sur ALL-IN</h1>

      <form @submit="onSubmit($event)" id="login-form">
            <div class="form-group">
                <label class="required">Pseudo : </label>
                <input type="text" class="form-control" required>
            </div>

        <div class="form-group">
            <label class="required">Mot de passe : </label>
            <input type="text" class="form-control" required>
            <span asp-validation-for="Password"></span>
        </div>

        <div class="form-group">
            <button type="submit" class="btn btn-dark btn-block">Se connecter</button>
        </div>
      </form>
</div>
</template>

<script>

import UserApiService from '../services/UserApiService.js'

 export default {
  
   methods: {
       async onSubmit(e) {
           e.preventDefault();

             try {                      
                    await this.executeAsyncRequest(() => UserApiService.Login(this.item));
                    this.$router.replace('/');
                 }
                  catch(error) {
                   }
               }
           }
       }
</script>

<style lang="scss">
  


</style>


  