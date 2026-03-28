---
title: "CodeCraftHub – Course Management REST API"
seoTitle: "CodeCraftHub – Beginner Flask REST API Project with CRUD"
seoDescription: "A beginner-friendly Flask REST API project with full CRUD operations using JSON storage—perfect for learning backend development."
datePublished: 2026-03-28T09:38:12.468Z
cuid: cmna50m7l00le29p8532qa5ei
slug: codecrafthub-course-management-rest-api
ogImage: https://cdn.hashnode.com/uploads/og-images/654a0052ae17ceea70d90f4b/49c699db-c8f0-49b7-a013-394ba8670518.png
tags: api, python, json, backend, python3, flask, beginners, jsonapi, rest-api, fastapi, backend-developments, api-development, python-backend, flaskapi, flask-app, crud-operations, codecrafthub, codecraft

---

Welcome to **CodeCraftHub** 💻✨  
This project is a simple **Flask-based REST API** that helps you manage courses using basic CRUD operations.

It is designed especially for **beginners** who want to learn:

*   How REST APIs work
    
*   How to use Flask
    
*   How to store data in JSON files
    

* * *

## 📌 1. Project Overview

CodeCraftHub is a backend API that allows users to:

*   Add new courses
    
*   View all courses
    
*   Get details of a specific course
    
*   Update course information
    
*   Delete courses
    

All data is stored locally in a file called `courses.json`.

* * *

## ✨ 2. Features

*   ✅ Simple REST API using Flask
    
*   ✅ Full CRUD functionality
    
*   ✅ JSON file-based storage (no database needed)
    
*   ✅ Auto-generated course IDs
    
*   ✅ Input validation (date format, status values)
    
*   ✅ Error handling (missing fields, invalid data, etc.)
    
*   ✅ Beginner-friendly structure
    

* * *

## ⚙️ 3. Installation Instructions

Follow these steps carefully 👇

### Step 1: Clone the repository

```plaintext
git clone https://github.com/your-username/codecrafthub.git
cd codecrafthub
```

### Step 2: Create a virtual environment (recommended)

```plaintext
python -m venv venv
```

### Step 3: Activate the virtual environment

**Windows:**

```plaintext
venv\Scripts\activate
```

**Mac/Linux:**

```plaintext
source venv/bin/activate
```

### Step 4: Install dependencies

```plaintext
pip install flask
```

* * *

## ▶️ 4. How to Run the Application

Start the Flask server:

```plaintext
python app.py
```

You should see output like:

```plaintext
Running on http://127.0.0.1:5000/
```

Your API is now live 🎉

* * *

## 🔗 5. API Endpoints Documentation

### 📌 Base URL

```plaintext
http://127.0.0.1:5000
```

* * *

### ➕ Create a Course

**POST /api/courses**

#### Request Body:

```plaintext
{
  "name": "Flask Basics",
  "description": "Learn Flask step by step",
  "target_date": "2026-04-10",
  "status": "Not Started"
}
```

#### Response:

```plaintext
{
  "id": 1,
  "name": "Flask Basics",
  "description": "...",
  "target_date": "2026-04-10",
  "status": "Not Started",
  "created_at": "2026-03-28T12:00:00"
}
```

* * *

### 📖 Get All Courses

**GET /api/courses**

* * *

### 🔍 Get Single Course

**GET /api/courses/{id}**

Example:

```plaintext
GET /api/courses/1
```

* * *

### ✏️ Update Course

**PUT /api/courses/{id}**

#### Request Body:

```plaintext
{
  "name": "Flask Advanced",
  "description": "Deep dive into Flask",
  "target_date": "2026-05-01",
  "status": "In Progress"
}
```

* * *

### ❌ Delete Course

**DELETE /api/courses/{id}**

* * *

### 📌 Allowed Status Values

*   Not Started
    
*   In Progress
    
*   Completed
    

* * *

## 🧪 6. Testing Instructions

You can test the API using:

### 🔹 Postman

*   Select method (GET, POST, etc.)
    
*   Enter URL
    
*   Add JSON body for POST/PUT
    
*   Click **Send**
    

### 🔹 cURL (Terminal)

Example:

```plaintext
curl -X GET http://127.0.0.1:5000/api/courses
```

* * *

## ⚠️ 7. Troubleshooting Common Issues

### ❌ Flask not installed

```plaintext
ModuleNotFoundError: No module named 'flask'
```

👉 Run:

```plaintext
pip install flask
```

* * *

### ❌ Port already in use

```plaintext
Address already in use
```

👉 Change port in [app.py](http://app.py):

```plaintext
app.run(debug=True, port=5001)
```

* * *

### ❌ Invalid status error

👉 Make sure status is one of:

*   Not Started
    
*   In Progress
    
*   Completed
    

* * *

### ❌ File errors

👉 Ensure:

*   `courses.json` exists (auto-created on first run)
    
*   App has permission to read/write files
    

* * *

## 📁 8. Project Structure

```plaintext
codecrafthub/
│
├── app.py           # Main Flask application
├── courses.json     # Data storage file (auto-created)
├── README.md        # Project documentation
└── venv/            # Virtual environment (optional)
```

* * *

## 🎯 Final Notes

This project is perfect if you are:

*   Learning backend development
    
*   Exploring REST APIs
    
*   Practicing Flask basics
    

Once you're comfortable, you can upgrade this project by:

*   Adding a database (SQLite, MongoDB)
    
*   Adding authentication
    
*   Deploying to cloud (Hugging Face, Render, etc.)
    

* * *

## ✨ 9. Source Code

from flask import Flask, jsonify, request import json import os from datetime import datetime

app = Flask(**name**)

DATA\_FILE = 'courses.json'

def load\_courses(): if not os.path.exists(DATA\_FILE): with open(DATA\_FILE, 'w') as f: json.dump(\[\], f) return \[\]

```plaintext
try:
    with open(DATA_FILE, 'r') as f:
        return json.load(f)
except json.JSONDecodeError:
    return []
```

def save\_courses(courses): try: with open(DATA\_FILE, 'w') as f: json.dump(courses, f, indent=2) return True except Exception as e: return False

def get\_next\_id(courses): if not courses: return 1 return max(course\['id'\] for course in courses) + 1

@app.route('/api/courses', methods=\['GET'\]) def get\_all\_courses(): courses = load\_courses() return jsonify({ 'success': True, 'count': len(courses), 'courses': courses }), 200

@app.route('/api/courses/[int:course\_id](int:course_id)', methods=\['GET'\]) def get\_course(course\_id): courses = load\_courses() course = next((c for c in courses if c\['id'\] == course\_id), None)

```plaintext
if course:
    return jsonify({'success': True, 'course': course}), 200
return jsonify({'success': False, 'error': 'Course not found'}), 404
```

@app.route('/api/courses', methods=\['POST'\]) def add\_course(): data = request.get\_json()

```plaintext
# Validate required fields
required_fields = ['name', 'description', 'target_date', 'status']
for field in required_fields:
    if field not in data:
        return jsonify({
            'success': False,
            'error': f'Missing required field: {field}'
        }), 400

# Validate status
valid_statuses = ['Not Started', 'In Progress', 'Completed']
if data['status'] not in valid_statuses:
    return jsonify({
        'success': False,
        'error': f'Status must be one of: {", ".join(valid_statuses)}'
    }), 400

courses = load_courses()

new_course = {
    'id': get_next_id(courses),
    'name': data['name'],
    'description': data['description'],
    'target_date': data['target_date'],
    'status': data['status'],
    'created_at': datetime.now().strftime('%Y-%m-%d %H:%M:%S')
}

courses.append(new_course)
save_courses(courses)

return jsonify({
    'success': True,
    'message': 'Course added successfully',
    'course': new_course
}), 201
```

@app.route('/api/courses/[int:course\_id](int:course_id)', methods=\['PUT'\]) def update\_course(course\_id): data = request.get\_json() courses = load\_courses()

```plaintext
course_index = next((i for i, c in enumerate(courses) if c['id'] == course_id), None)

if course_index is None:
    return jsonify({'success': False, 'error': 'Course not found'}), 404

# Update fields if provided
course = courses[course_index]
if 'name' in data:
    course['name'] = data['name']
if 'description' in data:
    course['description'] = data['description']
if 'target_date' in data:
    course['target_date'] = data['target_date']
if 'status' in data:
    course['status'] = data['status']

save_courses(courses)

return jsonify({
    'success': True,
    'message': 'Course updated successfully',
    'course': course
}), 200
```

@app.route('/api/courses/[int:course\_id](int:course_id)', methods=\['DELETE'\]) def delete\_course(course\_id): courses = load\_courses() course\_index = next((i for i, c in enumerate(courses) if c\['id'\] == course\_id), None)

```plaintext
if course_index is None:
    return jsonify({'success': False, 'error': 'Course not found'}), 404

deleted_course = courses.pop(course_index)
save_courses(courses)

return jsonify({
    'success': True,
    'message': 'Course deleted successfully',
    'deleted_course': deleted_course
}), 200
```

if **name** == '**main**': print("CodeCraftHub API is starting...") print(f"Data will be stored in: {os.path.abspath(DATA\_FILE)}") print("API will be available at: http://localhost:5000") app.run(debug=True, host='0.0.0.0', port=5000)