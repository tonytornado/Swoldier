import React, { useState } from 'react';
import MarkdownContainer from '../../../components/modules/MarkdownContainer';
import { Comment } from './Comment';
import { InputStack } from './InputStack';


/**
 * Renders a Post with comments and replies
 * 
 * @param {Array} props A comment, of course.
 */
export function Post(props) {
    const [post, setPost] = useState(props.post);
    const [comment, setComment] = useState(post.comments);

    if (post !== undefined) {
        return (
            <article key={post.postId}>
                <section>
                    <MarkdownContainer props={post.text} />
                    <div>
                        <small>{post.postDate} {post.Edited ?? "Edited"}</small>
                    </div>
                </section>
                <section>
                    {comment.map(c =>
                        <Comment comment={c} />
                    )}
                </section>
            </article>
        );
    }

    return (
        <InputStack type={"Entry"} />
    );
}


