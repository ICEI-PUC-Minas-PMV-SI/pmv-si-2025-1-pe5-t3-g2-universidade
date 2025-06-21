const tipo = localStorage.getItem('tipoUsuario');
if (tipo !== "2") {
  alert("Acesso restrito.");
  window.location.href = "index.html";
}
