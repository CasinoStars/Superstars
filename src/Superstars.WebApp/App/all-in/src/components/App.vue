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
              <i class="fa fa-bar-chart" style="font-size: 1.4rem;"></i> STATISTICS
            </router-link>
          </li>
          <li class="nav-item" v-if="auth.isConnected">
            <router-link class="nav-link" to="/play" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-gamepad" style="font-size: 1.4rem;"></i> JOUER
            </router-link>
          </li>
        </ul>

        <div class="collapse navbar-collapse" v-if="auth.isConnected">
          <ul class="navbar-nav ml-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/#" style="border-style: solid; border-width:0.7px; border-color: rgb(74, 133, 230); letter-spacing: 2px; font-size: 12px;">
                BANKROLL: {{BTCBankCoins}}<i class="fa fa-btc" style="font-size: 0.8rem;"></i> || {{fakeBankCoins}}<i class="fa fa-money" style="font-size: 0.8rem;"></i>
              </router-link>
            </li>
          </ul>
          <ul class="navbar-nav ml-auto">
            <li class="nav-item dropdown" style="text-transform: uppercase; letter-spacing: 1px; font-size: 12px;">
              <a class="nav-link dropdown-toggle" href="#" id="basic-nav-dropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                {{ auth.pseudo }}
              </a>
              <div role="menu" class="dropdown-menu dropdown-menu-right" aria-labelledby="basic-nav-dropdown" style="top:130%; right:-35%">
                <router-link class="dropdown-item" to="/wallet"><i class="fa fa-diamond"></i>     Porte-feuille</router-link>
                <router-link class="dropdown-item" to="/settings"><i class="fa fa-cog"></i>     Régagles</router-link>
                <div class="dropdown-divider"></div>
                <router-link class="dropdown-item" to="/logout"><i class="fa fa-sign-out"></i>     Déconnexion</router-link>
              </div>
            </li>
          </ul>
        </div>
        
        <div class="collapse navbar-collapse" v-else>
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
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import UserApiService from '../services/UserApiService';
import WalletApiService from '../services/WalletApiService';
import Vue from 'vue';

export default{
  data(){
    return {
      BTCBankCoins: 0,
      fakeBankCoins: 0,
    }
  },

  computed: {
    ...mapGetters(['isLoading']),
    auth: () => UserApiService
  },
  
  async mounted() {
    UserApiService.registerAuthenticatedCallback(() => this.onAuthenticated());
    await this.BTCBank();
    await this.fakeBank();
  },

  beforeDestroy() {
    UserApiService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),

    async BTCBank() {
      this.BTCBankCoins = await this.executeAsyncRequest(() => WalletApiService.GetBTCBankRoll());
    },

    async fakeBank() {
      this.fakeBankCoins = await this.executeAsyncRequest(() => WalletApiService.GetFakeBankRoll());
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


</style>

<style lang="scss">
@import "../styles/global.scss";
</style>