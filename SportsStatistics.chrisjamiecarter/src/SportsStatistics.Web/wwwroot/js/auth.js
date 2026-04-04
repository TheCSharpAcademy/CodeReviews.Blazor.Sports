// wwwroot/js/auth.js
export async function postSignin({ email, password, isPersistant }) {
    const response = await fetch("/api/identity/signin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email,
            password,
            isPersistant,
        })
    });

    const result = await response.json();
    return result;
}

export async function postDemoSignin({ email }) {
    const response = await fetch("/api/identity/demo/signin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email,
        })
    });

    const result = await response.json();
    return result;
}

export async function postSignout() {
    const xsrf = document.querySelector('meta[name="xsrf-token"]')?.content;
    const formData = new URLSearchParams();
    if (xsrf) {
        formData.append("__RequestVerificationToken", xsrf);
    }

    const response = await fetch("/api/identity/signout", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        credentials: "include",
        body: formData
    });

    const result = await response.json();
    return result;
}
