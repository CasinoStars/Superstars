<template>
  <div id="app">
    <header>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <router-link class="navbar-brand" to="/" style="font-family: 'Courier New', sans-serif; font-weight: 600px;">SUPERSTARS</router-link>
        
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        
        <ul class="nav navbar-nav">
          <li class="nav-item">
            <router-link class="nav-link" to="/statistics" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-bar-chart" style="font-size: 1.6rem;"></i> STATISTICS
            </router-link>
          </li>
        </ul>
        <div class="collapse navbar-collapse" id="navbarText" v-if="auth.isConnected">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/playersStats">Classement</router-link>
            </li>
          </ul>
          <ul class="navbar-nav my-2 my-md-0">
            <li class="nav-item dropdown" style="text-transform: uppercase; letter-spacing: 1px; font-size: 12px;">
              <a class="nav-link dropdown-toggle" href="#" id="basic-nav-dropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                {{ auth.pseudo }}
              </a>
              <div role="menu" class="dropdown-menu dropdown-menu-right" aria-labelledby="basic-nav-dropdown">
                <router-link class="dropdown-item" to="/wallet">Porte-feuille</router-link>
                <router-link class="dropdown-item" to="/settings">Régagles</router-link>
                <router-link class="dropdown-item" to="/logout">Se déconnecter</router-link>
              </div>
            </li>
          </ul>
        </div>
        <div class="collapse navbar-collapse" id="navbarText" v-else>
          <ul class="nav navbar-nav ml-auto">
            <li class="nav-item">
              <router-link class="nav-link" v-on:click.native="log('Login')" to="/">Login</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" v-on:click.native="log('Register')" to="/">Register</router-link>
            </li>
          </ul>
        </div>
      </nav>
      <div class="progress" v-if="isLoading">
        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%"></div>
      </div>
    </header>
    <router-view></router-view>
      <div class="games">
          <div class="yams">
            <img src="../img/LOGO1.png" alt="yams" id="imgyams">
            <img src="../img/LOGO2.png" alt="textyams" id="textyams">
          </div>
            <img src="../img/LOGO1.png" alt="poker" id="imgpoker">     
            <img src="../img/LOGO1.png" alt="blackjack" id="imgblackjack">
            <img src="../img/LOGO1.png" alt="game3" id="imggame3">     
            <img src="../img/LOGO1.png" alt="game4" id="imggame4">
      </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import UserApiService from '../services/UserApiService';
import Vue from 'vue';

export default{
  
  computed: {
    ...mapGetters(['isLoading']),
    auth: () => UserApiService
  },
  
  mounted() {
    UserApiService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  beforeDestroy() {
    UserApiService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {

    zoom() {

    },
    log(selectedBase) {
      UserApiService.log(selectedBase);
    },
    onAuthenticated() {
      this.$router.replace('/play');
    }
  }
}
</script>

<style lang="scss" scoped>

.app {
    background-color: black;
}

.progress {
  margin: 0px;
  padding: 0px;
  height: 5px;
}

a.router-link-active {
  font-weight: bold;
}

.games {
  margin: 290px;
  margin-left: 400px;
  display: -webkit-inline-box;
}

.textgames {
  margin-top: 190px;
  margin-left: 55px;
}

#textyams {
  height: 35px;
  width: 100px;
  transition: transform .4s;
  visibility: hidden;
}


#imgyams {
  transition: transform .4s;
  height: 180px;
  width: 215px;
}

#imgyams:hover{
    transform: scale(1.4);
}

.yams:hover #textyams {
visibility: visible;
}

#imgpoker {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imgpoker:hover {
    transform: scale(1.4); 
}

#imgblackjack {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imgblackjack:hover {
    transform: scale(1.4); 
}

#imggame3 {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imggame3:hover {
    transform: scale(1.4); 
}

#imggame4 {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imggame4:hover {
    transform: scale(1.4); 
}

</style>

<style lang="scss">
@import "../styles/global.scss";
</style>