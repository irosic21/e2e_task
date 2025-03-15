document.addEventListener("DOMContentLoaded", async function () {
    console.log("Loading employee list...");
    const employees = await fetchEmployees();
    
    const list = document.getElementById("employeeList");
    list.innerHTML = "";

    if (employees.length === 0) {
        list.innerHTML = "<li>No employees found</li>";
        return;
    }

    employees.forEach(emp => {
        const li = document.createElement("li");
        li.innerHTML = `
            <a class="employee-name" href="employee.html?id=${emp.id}">${emp.name} ${emp.surname}</a>
            <div class="department">${emp.contract.department}</div>
        `;
        
        li.addEventListener("click", () => {
            window.location.href = `employee.html?id=${emp.id}`;
        });

        list.appendChild(li);
    });

    console.log("Employee list displayed successfully.");
});
