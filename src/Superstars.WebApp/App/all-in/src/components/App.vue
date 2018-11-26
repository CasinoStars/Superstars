<template>
  <div id="app">
    <header>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <router-link class="navbar-brand" to="/" style="font-family: 'Courier New', sans-serif; font-weight: 600px;">ALL`IN</router-link>
        
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        
        <ul class="nav navbar-nav">
          <li class="nav-item">
            <router-link class="nav-link" to="/playersStats" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-trophy" style="font-size: 1.4rem;"></i> CLASSEMENT
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/rule" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-info-circle" style="font-size: 1.4rem;"></i> RÈGLES
            </router-link>
          </li>
          <li class="nav-item" v-if="auth.isConnected">
            <router-link class="nav-link" to="/statistics" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-bar-chart" style="font-size: 1.4rem;"></i> STATISTIQUES
            </router-link>
          </li>
          <li class="nav-item" v-if="auth.isConnected">
              <router-link class="nav-link" to="/ProvablyFair" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-balance-scale" style="font-size: 1.4rem;"></i> PROVABLYFAIR
              </router-link>
          </li>
        </ul>

        <div class="collapse navbar-collapse" v-if="auth.isConnected">
          <ul class="navbar-nav ml-auto">
            <li class="nav-item" v-if="auth.isConnected">
              <router-link class="nav-link" to="/play" style="letter-spacing: 2px; font-size: 12px;">
                <i class="fa fa-gamepad" style="font-size: 1.4rem;"></i> JOUER
              </router-link>
            </li>
          </ul>
          <ul class="navbar-nav ml-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/wallet" id="borderSolde" style="letter-spacing: 2px; font-size: 12px;">
                SOLDE DU COMPTE : {{BTCMoney.toLocaleString('en')}}<i class="fa fa-btc" style="font-size: 0.8rem;"></i> || {{fakeMoney.toLocaleString('en')}}<i class="fa fa-money" style="font-size: 0.8rem;"></i>
              </router-link>
            </li>
          </ul>
          <ul class="navbar-nav ml-auto">
            <li class="nav-item dropdown" style="text-transform: uppercase; letter-spacing: 1px; font-size: 12px;">
              <a class="nav-link dropdown-toggle" href="#" id="basic-nav-dropdown" role="button" data-toggle="modal" data-target="#settingsModal" aria-haspopup="true" aria-expanded="false">
                <i class="fa fa-user" style="font-size: 1.4rem;"></i> {{ auth.pseudo }}
              </a>
            <li class="nav-item">
              <router-link class="nav-link" to="/logout"><i class="fa fa-sign-out" style="font-size: 0.9rem;"></i></router-link>
            </li>
          </ul>
        </div>
        
        <div class="collapse navbar-collapse" v-else>
          <ul class="navbar-nav ml-auto">
            <li class="nav-item">
              <router-link class="nav-link" v-on:click.native="log('Login')" to="/#" style="letter-spacing: 2px; font-size: 12px;"><i class="fa fa-sign-in" style="font-size: 1.4rem;"></i> CONNEXION</router-link>
            </li>
          </ul>
        </div>
      </nav>
      <div class="progress" v-if="isLoading">
        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%"></div>
      </div>
    </header>
    <router-view></router-view>
    <chat v-if="auth.isConnected"></chat>
    <settings/>
  </div>
</template>
<script src="~/lib/signalr/signalr.js"></script>


<script>
import { mapGetters, mapActions } from 'vuex';
import UserApiService from '../services/UserApiService';
import WalletApiService from '../services/WalletApiService';
import Vue from 'vue';
import Chat from './Chat.vue'
import Settings from './Settings.vue'

export default{
  
  components: {
    Chat, Settings
  },

  computed: {
    ...mapGetters(['isLoading']),
    ...mapGetters(['BTCMoney']),
    ...mapGetters(['fakeMoney']),
    auth: () => UserApiService
  },
  
  async mounted() {
    if(UserApiService.isConnected) {
      await this.RefreshBTC();
      await this.RefreshFakeCoins();
    }
    
    UserApiService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  beforeDestroy() {
    UserApiService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),
    ...mapActions(['RefreshFakeCoins']),
    ...mapActions(['RefreshBTC']),
    
    log(selectedBase) {
      UserApiService.log(selectedBase);
    },
    
    async onAuthenticated() {
      await this.RefreshBTC();
      await this.RefreshFakeCoins();
      this.$router.replace('/play');
    },
  }
}
</script>

<style lang="scss" scoped>

.app {
    background-color: black;
}

#borderSolde{
  border-style:solid; 
  border-width:0.7px;
  border-color: rgb(74, 80, 180);
}

#borderSolde:hover{
  border-color: rgb(54, 114, 5);
}

.progress {
  margin: 0px;
  padding: 0px;
  height: 5px;
}

a.router-link-active {
  font-weight: bold;
}

</style>

<style lang="scss">
@import "../styles/global.scss";
</style>