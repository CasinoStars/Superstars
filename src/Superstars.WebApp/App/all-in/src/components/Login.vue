<template>
  <div id="home">
    <h1>Bienvenue sur ALL-IN</h1>

      <form @submit="onSubmit($event)" id="login-form">
        <input type="text" placeholder="Username" v-model="username" id="login"></input>
        <input type="password" placeholder="Password" v-model="password" id="password"></input>
        <button type="submit" mode="login" id="login-button">Login</button>
      </form>
</div>

</template>

<script>

import UserApiService from '../services/UserApiService'

export default {
  
  methods: {
      async onSubmit(e) {
          e.preventDefault();


            try {
                  if(this.mode == 'login') {
                     await this.executeAsyncRequest(() => UserApiService.IdentityVerify(this.item));
                      }
                     this.$router.replace('/Home');
                    }
                catch(error) {
                  }
              }
          }
      }
</script>

<style lang="css">
  
#login-form {
}

#login {
    width: 15%;
    padding: 12px 20px;
    margin: 20px 800px;
    display: inline-block;
    border: 2px solid grey;
  }

#password {
    width: 15%;
    padding: 12px 20px;
    margin: 20px 800px;
    display: inline-block;
    border: 2px solid grey;
}

#login-button {
    width: 15%;
    padding: 12px 20px;
    margin: 25px 800px;
    border: 3px solid white;
    }

</style>


  