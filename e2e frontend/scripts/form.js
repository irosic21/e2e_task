document.addEventListener("DOMContentLoaded", async function () {
    const params = new URLSearchParams(window.location.search);
    const isEdit = params.get("edit") === "true";
    const id = params.get("id");

    if (isEdit) {
        document.getElementById("formTitle").textContent = "Update Employee";
        const emp = await fetchEmployeeById(id);
        if (!emp) return;

        // Pre-fill placeholders (so fields remain empty if not changed)
        document.getElementById("name").placeholder = emp.name;
        document.getElementById("surname").placeholder = emp.surname;
        document.getElementById("gender").placeholder = emp.gender;
        document.getElementById("yearOfBirth").placeholder = emp.yearOfBirth;
        document.getElementById("contractType").placeholder = emp.contract.contractType;
        document.getElementById("department").placeholder = emp.contract.department;
        document.getElementById("duration").placeholder = emp.contract.durationOfContract;
        document.getElementById("dateOfEmployment").value = new Date(emp.contract.dateOfEmployment).toISOString().split("T")[0];
        document.getElementById("vacationDays").placeholder = emp.leaveRecords.vacationDays;
        document.getElementById("freeDays").placeholder = emp.leaveRecords.freeDays;
        document.getElementById("paidLeaveDays").placeholder = emp.leaveRecords.paidLeaveDays;
    }

    // ✅ Ensure the cancel button correctly redirects
    document.getElementById("cancelButton").addEventListener("click", function () {
        if (isEdit) {
            window.location.href = `employee.html?id=${id}`; // Redirect back to employee details if editing
        } else {
            window.location.href = "index.html"; // Redirect to home if adding a new employee
        }
    });

    document.getElementById("employeeForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        let formData = new FormData();

        if (!isEdit || document.getElementById("name").value) {
            formData.append("Name", document.getElementById("name").value);
        }
        if (!isEdit || document.getElementById("surname").value) {
            formData.append("Surname", document.getElementById("surname").value);
        }
        if (!isEdit || document.getElementById("gender").value) {
            formData.append("Gender", document.getElementById("gender").value);
        }
        if (!isEdit || document.getElementById("yearOfBirth").value) {
            formData.append("YearOfBirth", document.getElementById("yearOfBirth").value);
        }

        if (!isEdit || document.getElementById("dateOfEmployment").value) {
            formData.append("Contract.DateOfEmployment", document.getElementById("dateOfEmployment").value);
        }
        if (!isEdit || document.getElementById("contractType").value) {
            formData.append("Contract.ContractType", document.getElementById("contractType").value);
        }
        if (!isEdit || document.getElementById("duration").value) {
            formData.append("Contract.DurationOfContract", document.getElementById("duration").value);
        }
        if (!isEdit || document.getElementById("department").value) {
            formData.append("Contract.Department", document.getElementById("department").value);
        }

        if (!isEdit || document.getElementById("vacationDays").value) {
            formData.append("LeaveRecords.VacationDays", document.getElementById("vacationDays").value);
        }
        if (!isEdit || document.getElementById("freeDays").value) {
            formData.append("LeaveRecords.FreeDays", document.getElementById("freeDays").value);
        }
        if (!isEdit || document.getElementById("paidLeaveDays").value) {
            formData.append("LeaveRecords.PaidLeaveDays", document.getElementById("paidLeaveDays").value);
        }

        const fileInput = document.getElementById("picture");
        if (fileInput.files.length > 0) {
            formData.append("imageFile", fileInput.files[0]);
        }

        const success = await submitEmployeeForm(formData, isEdit, id);

        if (success) {
            alert(`Employee ${isEdit ? "updated" : "added"} successfully`);
            window.location.href = isEdit ? `employee.html?id=${id}` : "index.html";
        } else {
            alert("Failed to submit employee data.");
        }
    });
});
