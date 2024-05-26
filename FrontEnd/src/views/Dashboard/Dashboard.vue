<script setup>
import { ref, onMounted, defineAsyncComponent, computed } from "vue";
const blogs=ref([])
// import BlogService from "@/service/BlogService";
// const blogService = new BlogService();

onMounted(()=>{
  getList()
})

const getList = (args) => {

blogService.GetBlogs().then((response) => {
  if (response) {
    blogs.value = response;
  }
});
};



</script>
<template>
  <div class="grid">
    <div class="col-12">
      test
    </div> 
  </div>  
  
</template>
