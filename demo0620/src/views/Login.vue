<template>
  <div class="login-container">
    <el-form
      :model="form"
      status-icon
      :rules="rules"
      ref="form"
      label-width="100px"
      class="login-form"
    >
      <h1 class="title-zc">登录页</h1>
      <el-form-item label="账号" prop="UserName">
        <el-input v-model.number="form.UserName"></el-input>
      </el-form-item>
 
      <el-form-item label="密码" prop="PassWord">
        <el-input
          type="password"
          v-model="form.PassWord"
          autocomplete="off"
        ></el-input>
      </el-form-item>

      <el-form-item label="验证码" prop="name">
        <el-input style="width: 147px;margin-top:10px" v-model="form.CodeMa" placeholder="请填写验证码"></el-input>
            <img :src="this.imgUrl" alt="" @click="graphicCode">
        </el-form-item>

 
      <el-form-item>
        <el-button type="primary" @click="submitForm">提交</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<script>
 
export default {
  data() {
    return {
      form: {
        UserName: "",
        PassWord: "",
        CodeMa:''
      },
      rules: {
        UserName: [{ required: true, message: "请输入账号", trigger: "blur" }],
        PassWord: [{ required: true, message: "请输入密码", trigger: "blur" }],
      },
      imgUrl:''
    };
  },
  methods: {
    //登陆
    submitForm() {
      debugger;
        this.$axios.post("/api/Login/GetLogin",this.form).then(res=>{
            let t=res.data;
            if(t.Code>0)
            {
                this.$message.error(t.Mes);
            }
            else
            {
                sessionStorage.setItem("UserName",this.form.UserName);
                localStorage.setItem("LoginToken",t.LoginToken);

                this.$router.push("/Home");
                 this.$message.success(t.Mes);
            }
        })
    },
    //验证码
    graphicCode() {
        this.$axios.get('/api/VFCode/Captcha', {responseType: 'blob'}).then((response) => {
            this.imgUrl = window.URL.createObjectURL(response.data)
        })
    }

  },
  created(){
    this.graphicCode();
  }
};
</script>
<style scoped>
.login-container {
  position: absolute;
  width: 100%;
  height: 100%;
  overflow: hidden;
  /* background-image: url("../../assets/bgg.jpg"); */
  background-size: 100% 100%;
  background-repeat: no-repeat;
}
.login-form {
  width: 350px;
  margin: 150px auto;
  background-color: rgba(90, 187, 211, 0.7);
  padding: 30px;
  border-radius: 20px;
}
.title-zc {
  text-align: center;
}
</style>