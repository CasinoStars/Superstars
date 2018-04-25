<template>
  <div id="home">
    <h1>Bienvenue sur ALL-IN</h1>

      <form id="login-form">
        <input type="text" placeholder="Username" v-model="username" id="login"></input>
        <input type="password" placeholder="Password" v-model="password" id="password"></input>
        <button type="submit">Login</button>
      </form>
  </div>

</template>

<style lang="css">
  
#login-form {
}

#login {
    margin: center;
    border: 3px solid grey;
  }

#password {
    margin: 120px;
    border: 3px solid grey;
}
</style>


  