const apiBase = "http://localhost:5039/api/StudentAPI/";

const form = document.getElementById("studentForm");
const nameInput = document.getElementById("name");
const ageInput = document.getElementById("age");
const gradeInput = document.getElementById("grade");
const idInput = document.getElementById("studentId");
const tableBody = document.querySelector("#studentsTable tbody");
const passedButton = document.getElementById("passedButton");
form.addEventListener("submit", async (e) => {
  e.preventDefault();
  const student = {
    name: nameInput.value,
    age: parseInt(ageInput.value),
    grade: parseInt(gradeInput.value),
  };
  if (idInput.value) {
    await updateStudent(idInput.value, student);
  } else {
    await addStudent(student);
  }
  form.reset();
  idInput.value = "";
  loadStudents();
});

async function loadStudents() {
  try {
    const res = await fetch(apiBase + "All");
    if (!res.ok) throw new Error("Failed to fetch students.");
    const students = await res.json();
    tableBody.innerHTML = "";
    students.forEach((s) => {
      const row = document.createElement("tr");
      row.innerHTML = `
        <td>${s.id}</td>
        <td>${s.name}</td>
        <td>${s.age}</td>
        <td>${s.grade}</td>
        <td class="actions">
          <button onclick='editStudent(${JSON.stringify(s)})'>Edit</button>
          <button onclick='deleteStudent(${s.id})'>Delete</button>
        </td>
      `;
      tableBody.appendChild(row);
    });
  } catch (err) {
    console.error("Error loading students:", err);
  }
}

function editStudent(student) {
  idInput.value = student.id;
  nameInput.value = student.name;
  ageInput.value = student.age;
  gradeInput.value = student.grade;
}

async function addStudent(student) {
  try {
    const res = await fetch(apiBase + "Add", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(student),
    });
    if (res.ok) {
      const added = await res.json();
      console.log("Added:", added);
    } else if (res.status === 400) {
      alert("Invalid student data.");
    }
  } catch (err) {
    console.error("Add error:", err);
  }
}

async function updateStudent(id, student) {
  try {
    const res = await fetch(apiBase + "Update/" + id, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(student),
    });
    if (res.ok) {
      const updated = await res.json();
      console.log("Updated:", updated);
    } else if (res.status === 400) {
      alert("Invalid update data.");
    } else if (res.status === 404) {
      alert("Student not found.");
    }
  } catch (err) {
    console.error("Update error:", err);
  }
}

async function deleteStudent(id) {
  if (!confirm("Delete student?")) return;
  try {
    const res = await fetch(apiBase + "Delete/" + id, { method: "DELETE" });
    if (res.ok) {
      console.log(`Student ${id} deleted.`);
      loadStudents();
    } else if (res.status === 404) {
      alert("Student not found.");
    } else if (res.status === 400) {
      alert("Bad request.");
    }
  } catch (err) {
    console.error("Delete error:", err);
  }
}



loadStudents();
