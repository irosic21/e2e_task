document.addEventListener("DOMContentLoaded", async function () {
    const params = new URLSearchParams(window.location.search);
    const id = params.get("id");
    const picUrl = "http://localhost:5062/pictures/";
    
    if (!id) return;

    console.log(`Loading details for employee ID: ${id}`);
    
    const emp = await fetchEmployeeById(id);
    if (!emp) {
        console.error("Employee not found.");
        return;
    }

    // ✅ Display Name & Surname together as "Marko da"
    document.getElementById("empFullName").textContent = `${emp.name} ${emp.surname}`;
    document.getElementById("empGender").textContent = emp.gender;
    document.getElementById("empYearOfBirth").textContent = emp.yearOfBirth;

    // ✅ Handle profile picture (fallback if missing)
    const imgElement = document.getElementById("employeePic");
    imgElement.src = emp.picture ? picUrl + emp.picture : "default-profile.png";
    imgElement.onerror = () => imgElement.src = "default-profile.png"; // Fallback if image fails to load

    // ✅ Load Leave Records
    document.getElementById("vacationDays").textContent = emp.leaveRecords.vacationDays;
    document.getElementById("freeDays").textContent = emp.leaveRecords.freeDays;
    document.getElementById("paidLeaveDays").textContent = emp.leaveRecords.paidLeaveDays;

    // ✅ Load Contract Details
    document.getElementById("empDepartment").textContent = emp.contract.department;
    document.getElementById("contractType").textContent = emp.contract.contractType;
    document.getElementById("dateOfEmployment").textContent = new Date(emp.contract.dateOfEmployment).toLocaleDateString();
    document.getElementById("duration").textContent = emp.contract.durationOfContract;

    // ✅ Delete Employee Functionality
    document.getElementById("deleteButton").addEventListener("click", async () => {
        const confirmed = confirm("Are you sure you want to delete this employee?");
        if (!confirmed) return;

        const success = await deleteEmployee(id);
        if (success) {
            alert("Employee deleted successfully.");
            window.location.href = "index.html"; // Redirect after deletion
        } else {
            alert("Failed to delete employee.");
        }
    });

    console.log("Employee details displayed successfully.");
});
